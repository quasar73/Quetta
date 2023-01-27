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
        private readonly IHubContext<MessageHub> messageHubContext;
        private readonly IHubContext<SidebarHub> sidebarHubContext;
        private readonly QuettaDbContext dbContext;
        private readonly IBaseEncryptingService encryptingService;
        private readonly IMapper mapper;

        public MessageHandler(
            IHubContext<MessageHub> messageHubContext,
            IHubContext<SidebarHub> sidebarHubContext,
            QuettaDbContext dbContext,
            IMapper mapper,
            IBaseEncryptingService encryptingService
        )
        {
            this.messageHubContext = messageHubContext;
            this.sidebarHubContext = sidebarHubContext;
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
                .Include(m => m.Chat)
                .ThenInclude(c => c.Users)
                .FirstOrDefault(m => m.Id == notification.MessageId);

            if (message == null)
            {
                throw new EntityNotFoundException();
            }

            var chat = dbContext.Chats
                .Include(c => c.Messages)
                .ThenInclude(m => m.WhoRead)
                .FirstOrDefault(c => c.Id == message!.ChatId);

            var text = await encryptingService.Decrypt(message.Text, message.SecretVersion);
            var messageResponse = mapper.Map<MessageResponse>(message);
            messageResponse.Text = text ?? "";
            messageResponse.IsSupported = text != null;

            var sidebarResponse = mapper.Map<SidebarResponse>(message);
            sidebarResponse.Text = text ?? "";

            await messageHubContext.Clients
                .Group(notification.ChatId)
                .SendAsync("NewMessage", messageResponse);

            foreach (var user in message.Chat.Users)
            {
                sidebarResponse.Amount = chat!.Messages
                    .Where(
                        m => m.UserId != user.Id && !m.WhoRead.Select(wr => wr.Id).Contains(user.Id)
                    )
                    .Count();
                await sidebarHubContext.Clients
                    .Group(user.Id)
                    .SendAsync("NewMessage", sidebarResponse);
            }

            return;
        }
    }
}
