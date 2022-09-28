using MediatR;

namespace Quetta.Common.Models.Commands
{
    public class DeleteMessagesCommand : IRequest
    {
        public string UserId { get; set; }

        public List<string> MessageIds { get; set; }

        public DeleteMessagesCommand() { }

        public DeleteMessagesCommand(string userId, string[] messageIds) 
        {
            UserId = userId;
            MessageIds = messageIds.ToList();
        }
    }
}
