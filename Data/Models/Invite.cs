using Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Invite
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public bool IsGroupChat { get; set; }

        [Required]
        public InviteStatus Status { get; set; }

        public string? ChatId { get; set; }

        public string SenderId { get; set; }

        public string ReceiverId { get; set; }

        public virtual Chat? Chat { get; set; }

        [ForeignKey("SenderId")]
        public virtual User Sender { get; set; }

        [ForeignKey("ReceiverId")]
        public virtual User Receiver { get; set; }
    }
}
