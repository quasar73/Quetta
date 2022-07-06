namespace Common.Models.Responses
{
    public class InviteResponse
    {
        public string SenderUsername { get; set; }

        public string InviteId { get; set; }

        public string? ChatId { get; set; }
        
        public bool IsGroupChat { get; set; }
    }
}
