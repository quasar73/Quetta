using Quetta.Common.Enums;
using Quetta.Data;
using Quetta.Data.Models;
using Quetta.Logic.Hubs;
using Quetta.Common.Models.Notifications;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Quetta.Logic.NotificationHandlers
{
    public class NewNotificationHandler : INotificationHandler<NewNotification>
    {
        private readonly UserManager<User> userManager;
        private readonly IHubContext<NotificationHub> hubContext;
        private readonly QuettaDbContext dbContext;

        public NewNotificationHandler(UserManager<User> userManager, IHubContext<NotificationHub> hubContext, QuettaDbContext dbContext)
        {
            this.hubContext = hubContext;
            this.userManager = userManager;
            this.dbContext = dbContext;
        }

        public async Task Handle(NewNotification notification, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByNameAsync(notification.ReceiverUsername);
            var hasNotifications = dbContext.Invites
                                    .Include(i => i.Receiver)
                                    .Any(i => i.Receiver.UserName == notification.ReceiverUsername && i.Status == InviteStatus.Pending);

            await hubContext.Clients.Group(user.Id).SendAsync("Notify", hasNotifications);
        }
    }
}
