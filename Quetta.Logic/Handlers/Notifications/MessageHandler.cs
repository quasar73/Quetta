using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Quetta.Common.Exceptions;
using Quetta.Common.Models.Notifications;
using Quetta.Common.Models.Responses;
using Quetta.Data;
using Quetta.Logic.Hubs;

namespace Quetta.Logic.Handlers.Notifications
{
    public class MessageHandler : INotificationHandler<MessageNotification>
    {
        private readonly IHubContext<MessageHub> hubContext;
        private readonly QuettaDbContext dbContext;
        private readonly IMapper mapper;

        public MessageHandler(IHubContext<MessageHub> hubContext, QuettaDbContext dbContext, IMapper mapper)
        {
            this.hubContext = hubContext;
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task Handle(MessageNotification notification, CancellationToken cancellationToken)
        {
            var message = dbContext.Messages.Include(m => m.User).FirstOrDefault(m => m.Id == notification.MessageId);

            if (message == null)
            {
                throw new EntityNotFoundException();
            }

            var messageResponse = mapper.Map<MessageResponse>(message);
            await hubContext.Clients.Group(notification.ChatId).SendAsync("NewMessage", messageResponse);
            return;
        }
    }
}
