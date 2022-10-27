using MediatR;

namespace Quetta.Common.Models.Commands
{
    public class ReadMessagesCommand : IRequest<Unit>
    {
        public string MessageId { get; set; }

        public string UserId { get; set; }

        public ReadMessagesCommand(string messageId, string userId) 
        {
            MessageId = messageId;
            UserId = userId;
        }

        public ReadMessagesCommand() { }
    }
}
