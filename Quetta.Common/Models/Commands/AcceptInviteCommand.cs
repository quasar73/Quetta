using MediatR;

namespace Quetta.Common.Models.Commands
{
    public class AcceptInviteCommand : IRequest
    {
        public string InviteId { get; set; }

        public string ReceiverId { get; set; }

        public AcceptInviteCommand(string inviteId, string receiverId)
        {
            InviteId = inviteId;
            ReceiverId = receiverId;
        }

        public AcceptInviteCommand() { }
    }
}
