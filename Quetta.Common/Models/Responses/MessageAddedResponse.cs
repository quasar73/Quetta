namespace Quetta.Common.Models.Responses
{
    public class MessageAddedResponse
    {
        public string MessageId { get; set; }

        public MessageAddedResponse(string messageId)
        {
            MessageId = messageId;
        }
    }
}
