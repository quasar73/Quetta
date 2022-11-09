using MediatR;
using Quetta.Common.Models.Notifications;
using Microsoft.AspNetCore.SignalR;
using Quetta.Logic.Hubs;

namespace Quetta.Logic.Handlers.Notifications
{
    public class ReadHandler : INotificationHandler<ReadNotification>
    {
        private readonly IHubContext<ReadHub> hubContext;

        public ReadHandler(IHubContext<ReadHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public async Task Handle(ReadNotification notification, CancellationToken cancellationToken)
        {
            await hubContext.Clients
                .Group(notification.ChatId)
                .SendAsync("ReadMessages", notification.MessageIds);
        }
    }
}
