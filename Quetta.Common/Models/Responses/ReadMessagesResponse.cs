namespace Quetta.Common.Models.Responses
{
    public class ReadMessagesResponse
    {
        public string[] MessageIds { get; set; }

        public ReaderResponse Reader { get; set; }
    }
}
