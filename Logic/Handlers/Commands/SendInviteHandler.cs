using Common.Enums;
using Common.Exceptions;
using Common.Models.Commands;
using Data;
using Data.Models;
using Logic.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Identity;

namespace Logic.Handlers.Commands
{
    public class SendInviteHandler : IRequestHandler<SendInviteCommand>
    {
        private readonly QuettaDbContext dbContext;
        private readonly UserManager<User> userManager;

        public SendInviteHandler(QuettaDbContext dbContext, UserManager<User> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public async Task<Unit> Handle(SendInviteCommand request, CancellationToken cancellationToken)
        {
            var receiver = await userManager.FindByNameAsync(request.SendInviteRequest.ReceiverUsername);

            if (receiver == null)
            {
                throw new EntityNotFoundException("User not found.");
            }

            var isNotificationExist = dbContext.Invites.Any(i => i.ReceiverId == receiver.Id && i.SenderId == request.SenderId && !i.IsGroupChat && i.Status != InviteStatus.Declined);

            if (isNotificationExist)
            {
                throw new UserAlreadyInvitedException();
            }

            var invite = new Invite
            {
                Id = Guid.NewGuid().ToString(),
                IsGroupChat = request.SendInviteRequest.IsGroupChat,
                Status = InviteStatus.Pending,
                ChatId = request.SendInviteRequest.ChatId,
                SenderId = request.SenderId,
                ReceiverId = receiver.Id,
            };

            dbContext.Invites.Add(invite);
            await dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
