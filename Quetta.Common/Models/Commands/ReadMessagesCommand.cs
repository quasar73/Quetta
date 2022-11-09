using MediatR;
using Quetta.Common.Models.Responses;

namespace Quetta.Common.Models.Commands
{
    public class ReadMessagesCommand : IRequest<ReadMessagesResponse>
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
