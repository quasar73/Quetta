namespace Data.Models
{
    public class RefreshToken
    {
        public string Id { get; set; }

        public string Token { get; set; }
        
        public DateTime Expires { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
