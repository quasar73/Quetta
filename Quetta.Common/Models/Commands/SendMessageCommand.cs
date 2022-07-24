namespace Quetta.Common.Models.Commands
{
    public class SendMessageCommand
    {
        public string Text { get; set; }

        public DateTime Date { get; set; }

        public string SenderId { get; set; }
    }
}
