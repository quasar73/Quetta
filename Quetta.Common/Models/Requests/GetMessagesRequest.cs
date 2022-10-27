namespace Quetta.Common.Models.Requests
{
    // TODO: Add validation
    public class GetMessagesRequest
    {
        public string ChatId { get; set; }

        public string? LastMessageId { get; set; }

        public int Amount { get; set; }
    }
}
