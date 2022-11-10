using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Quetta.Common.Exceptions;
using Quetta.Common.Models.Commands;
using Quetta.Common.Models.Notifications;
using Quetta.Common.Models.Responses;
using Quetta.Data;
using Quetta.Data.Models;

namespace Quetta.Logic.Handlers.Commands
{
    public class ReadMessagesHandler : IRequestHandler<ReadMessagesCommand, Unit>
    {
        private readonly QuettaDbContext dbContext;
        private readonly UserManager<User> userManager;
        private readonly IMediator mediator;

        public ReadMessagesHandler(
            QuettaDbContext dbContext,
            UserManager<User> userManager,
            IMediator mediator
        )
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.mediator = mediator;
        }

        public async Task<Unit> Handle(
            ReadMessagesCommand request,
            CancellationToken cancellationToken
        )
        {
            var message = dbContext.Messages
                .Include(m => m.Chat)
                .ThenInclude(c => c.Users)
                .Include(m => m.Chat)
                .ThenInclude(c => c.Messages)
                .ThenInclude(m => m.WhoRead)
                .FirstOrDefault(m => m.Id == request.MessageId);

            if (message == null)
            {
                throw new EntityNotFoundException();
            }

            var chat = message.Chat;

            if (!chat.Users.Any(u => u.Id == request.UserId))
            {
                throw new AccessDeniedException();
            }

            var user = await userManager.FindByIdAsync(request.UserId);
            var unreadMessages = chat.Messages
                .Where(
                    m =>
                        m.Date <= message.Date
                        && !m.WhoRead.Any(wr => wr.Id == request.UserId)
                        && m.UserId != request.UserId
                )
                .ToList();
            unreadMessages.ForEach(m => m.WhoRead.Add(user));

            await dbContext.SaveChangesAsync();

            await mediator.Publish(new ReadNotification
            {
                MessageIds = unreadMessages.Select(u => u.Id).ToArray(),
                ChatId = chat.Id,
                Reader = new()
                {
                    AvatarUrl = user.AvatarUrl,
                    FullName = $"{user.FirstName} {user.LastName}",
                }
            });

            return Unit.Value;
        }
    }
}
