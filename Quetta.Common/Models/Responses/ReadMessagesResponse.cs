namespace Quetta.Common.Models.Responses
{
    // TODO: Validation
    public class ReadMessagesResponse
    {
        public string[] MessageIds { get; set; }

        public ReaderResponse Reader { get; set; }
    }
}
