using Quetta.Data;
using MediatR;
using Quetta.Common.Models.Commands;
using Quetta.Data.Models;

namespace Quetta.Logic.Handlers.Commands
{
    public class SendMessageHandler : IRequestHandler<SendMessageCommand, string>
    {
        private readonly QuettaDbContext dbContext;

        public SendMessageHandler(QuettaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            var message = new Message
            {
                Id = Guid.NewGuid().ToString(),
                Text = request.Text,
                Date = request.Date,
                UserId = request.SenderId,
                ChatId = request.ChatId,
            };

            dbContext.Messages.Add(message);
            await dbContext.SaveChangesAsync();

            return message.Id;
        }
    }
}
