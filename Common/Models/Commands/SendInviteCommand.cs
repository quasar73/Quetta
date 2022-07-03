using Common.Models.Requests;
using MediatR;

namespace Common.Models.Commands
{
    public class SendInviteCommand : IRequest
    {
        public SendInviteRequest SendInviteRequest { get; set; }

        public string SenderId { get; set; }

        public SendInviteCommand() { }

        public SendInviteCommand(SendInviteRequest SendInviteRequest, string SenderId)
        {
            this.SendInviteRequest = SendInviteRequest;
            this.SenderId = SenderId;
        }
    }
}
