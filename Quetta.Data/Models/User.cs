using Microsoft.AspNetCore.Identity;

namespace Quetta.Data.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string? AvatarUrl { get; set; }

        public virtual ICollection<Chat> Chats { get; set; }
        
        public virtual ICollection<Chat> CreatedChats { get; set; }

        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }

        public virtual ICollection<Invite> InvitesIncoming { get; set; }

        public virtual ICollection<Invite> InvitesOutcoming { get; set; }
    }
}
