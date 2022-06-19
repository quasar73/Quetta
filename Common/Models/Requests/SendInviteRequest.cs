using System.ComponentModel.DataAnnotations;

namespace Common.Models.Requests
{
    public class SendInviteRequest
    {
        [Required]
        public string ReceiverUsername { get; set; }
        
        [Required]
        public bool IsGroupChat { get; set; }        

        public string? ChatId { get; set; }
    }
}
