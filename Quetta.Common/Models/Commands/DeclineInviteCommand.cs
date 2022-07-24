using MediatR;

namespace Quetta.Common.Models.Commands
{
    public class DeclineInviteCommand : IRequest
    {
        public string InviteId { get; set; }

        public string ReceiverId { get; set; }

        public DeclineInviteCommand(string inviteId, string receiverId)
        {
            InviteId = inviteId;
            ReceiverId = receiverId;
        }

        public DeclineInviteCommand() { }
    }
}
