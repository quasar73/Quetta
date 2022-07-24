namespace  Quetta.Common.Models.Requests
{
    public class SendInviteRequest
    {
        public string ReceiverUsername { get; set; }
        
        public bool IsGroupChat { get; set; }        

        public string? ChatId { get; set; }
    }
}
