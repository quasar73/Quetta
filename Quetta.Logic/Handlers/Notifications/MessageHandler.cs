using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Quetta.Common.Exceptions;
using Quetta.Common.Models.Notifications;
using Quetta.Common.Models.Responses;
using Quetta.Data;
using Quetta.Logic.Hubs;
using Quetta.Logic.Interfaces;

namespace Quetta.Logic.Handlers.Notifications
{
    public class MessageHandler : INotificationHandler<MessageNotification>
    {
        private readonly IHubContext<MessageHub> hubContext;
        private readonly QuettaDbContext dbContext;
        private readonly IBaseEncryptingService encryptingService;
        private readonly IMapper mapper;

        public MessageHandler(
            IHubContext<MessageHub> hubContext,
            QuettaDbContext dbContext,
            IMapper mapper,
            IBaseEncryptingService encryptingService
        )
        {
            this.hubContext = hubContext;
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.encryptingService = encryptingService;
        }

        public async Task Handle(
            MessageNotification notification,
            CancellationToken cancellationToken
        )
        {
            var message = dbContext.Messages
                .Include(m => m.User)
                .FirstOrDefault(m => m.Id == notification.MessageId);

            if (message == null)
            {
                throw new EntityNotFoundException();
            }

            var text = await encryptingService.Decrypt(message.Text, message.SecretVersion);
            var messageResponse = mapper.Map<MessageResponse>(message);
            messageResponse.Text = text ?? "";
            messageResponse.IsSupported = text != null;

            await hubContext.Clients
                .Group(notification.ChatId)
                .SendAsync("NewMessage", messageResponse);
            return;
        }
    }
}
