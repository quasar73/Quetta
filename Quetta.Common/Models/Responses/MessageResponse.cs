namespace Quetta.Common.Models.Responses
{
    public class MessageResponse
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public string Username { get; set; }

        public DateTime Date { get; set; }

        public bool IsSupported { get; set; }

        public ICollection<ReaderResponse> Readers { get; set; }
    }
}
