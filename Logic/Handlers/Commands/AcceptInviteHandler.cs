using Common.Enums;
using Common.Exceptions;
using Data;
using Data.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Quetta.Common.Models.Commands;

namespace Quetta.Logic.Handlers.Commands
{
    public class AcceptInviteHandler : IRequestHandler<AcceptInviteCommand, Unit>
    {
        private readonly QuettaDbContext dbContext;
        private readonly UserManager<User> userManager;

        public AcceptInviteHandler(QuettaDbContext dbContext, UserManager<User> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public async Task<Unit> Handle(AcceptInviteCommand request, CancellationToken cancellationToken)
        {
            var invite = dbContext.Invites.FirstOrDefault(i => i.Id == request.InviteId);

            if (invite == null || invite.ReceiverId != request.ReceiverId)
            {
                throw new EntityNotFoundException();
            }

            invite.Status = InviteStatus.Accepted;
            dbContext.Invites.Update(invite);

            if (invite.IsGroupChat == false && invite.ChatId == null)
            {
                var users = new List<User>();
                users.Add(await userManager.FindByIdAsync(invite.SenderId));
                users.Add(await userManager.FindByIdAsync(invite.ReceiverId));

                var chat = new Chat
                {
                    Id = Guid.NewGuid().ToString(),
                    IsGroup = false,
                    Users = users
                };

                dbContext.Chats.Add(chat);
            }

            await dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
