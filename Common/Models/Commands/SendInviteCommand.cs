using MediatR;

namespace Common.Models.Commands
{
    public class SendInviteCommand : IRequest
    {
        public string ReceiverId { get; set; }
        public bool IsGroupChat { get; set; }
        public string? ChatId { get; set; }
    }
}
