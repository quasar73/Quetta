namespace Quetta.Common.Models.Requests
{
    public class GetMessagesRequest
    {
        public string ChatId { get; set; }

        public string? LastMessageId { get; set; }

        public int Amount { get; set; }
    }
}
