using Common.Enums;

namespace Data.Models
{
    public class Invite
    {
        public string Id { get; set; }

        public bool IsGroupChat { get; set; }

        public InviteStatus Status { get; set; }

        public DateTime DateTime { get; set; } 

        public string? ChatId { get; set; }

        public virtual Chat? Chat { get; set; }

        public string SenderId { get; set; }

        public virtual User Sender { get; set; }

        public string ReceiverId { get; set; }

        public virtual User Receiver { get; set; }
    }
}
