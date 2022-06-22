using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Message
    {
        [Key]
        public string Id { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public string ChatId { get; set; }

        public virtual Chat Chat { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
