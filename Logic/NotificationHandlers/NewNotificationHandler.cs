using Data;
using Data.Models;
using Logic.Hubs;
using Logic.Notifications;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace Logic.NotificationHandlers
{
    public class NewNotificationHandler : INotificationHandler<NewNotification>
    {
        private readonly UserManager<User> userManager;
        private readonly IHubContext<NotificationHub> hubContext;

        public NewNotificationHandler(UserManager<User> userManager, IHubContext<NotificationHub> hubContext)
        {
            this.hubContext = hubContext;
            this.userManager = userManager;
        }

        public async Task Handle(NewNotification notification, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByNameAsync(notification.ReceiverUsername);
            await hubContext.Clients.Group(user.Id).SendAsync("Notify", true);
        }
    }
}
