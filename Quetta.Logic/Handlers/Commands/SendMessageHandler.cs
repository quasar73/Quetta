using Quetta.Data;
using MediatR;
using Quetta.Common.Models.Commands;
using Quetta.Data.Models;
using Quetta.Logic.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Quetta.Logic.Handlers.Commands
{
    public class SendMessageHandler : IRequestHandler<SendMessageCommand, string>
    {
        private readonly QuettaDbContext dbContext;
        private readonly IBaseEncryptingService encryptingService;
        private readonly IConfiguration configuration;

        public SendMessageHandler(
            QuettaDbContext dbContext,
            IBaseEncryptingService encryptingService,
            IConfiguration configuration
        )
        {
            this.configuration = configuration;
            this.dbContext = dbContext;
            this.encryptingService = encryptingService;
        }

        public async Task<string> Handle(
            SendMessageCommand request,
            CancellationToken cancellationToken
        )
        {
            var encryptedMessage = await encryptingService.Encrypt(request.Text);

            var message = new Message
            {
                Id = Guid.NewGuid().ToString(),
                Text = encryptedMessage,
                Date = request.Date,
                UserId = request.SenderId,
                ChatId = request.ChatId,
                SecretVersion = configuration["Crypt:ActualSecret"],
            };

            dbContext.Messages.Add(message);
            await dbContext.SaveChangesAsync();

            return message.Id;
        }
    }
}
