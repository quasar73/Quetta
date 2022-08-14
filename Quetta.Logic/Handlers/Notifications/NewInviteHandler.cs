using Quetta.Common.Enums;
using Quetta.Data;
using Quetta.Data.Models;
using Quetta.Logic.Hubs;
using Quetta.Common.Models.Notifications;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Quetta.Logic.Handlers.Notifications
{
    public class NewInviteHandler : INotificationHandler<NewInviteNotification>
    {
        private readonly UserManager<User> userManager;
        private readonly IHubContext<InviteHub> hubContext;
        private readonly QuettaDbContext dbContext;

        public NewInviteHandler(UserManager<User> userManager, IHubContext<InviteHub> hubContext, QuettaDbContext dbContext)
        {
            this.hubContext = hubContext;
            this.userManager = userManager;
            this.dbContext = dbContext;
        }

        public async Task Handle(NewInviteNotification notification, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByNameAsync(notification.ReceiverUsername);
            var hasNotifications = dbContext.Invites
                                    .Include(i => i.Receiver)
                                    .Any(i => i.Receiver.UserName == notification.ReceiverUsername && i.Status == InviteStatus.Pending);

            await hubContext.Clients.Group(user.Id).SendAsync("Notify", hasNotifications);
        }
    }
}
