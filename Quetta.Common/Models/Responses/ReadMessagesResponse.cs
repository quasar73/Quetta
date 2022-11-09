namespace Quetta.Common.Models.Responses
{
    public class ReadMessagesResponse
    {
        public string ChatId { get; set; }

        public string[] MessageIds { get; set; }
    }
}
