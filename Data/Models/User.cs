using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        public string? AvatarUrl { get; set; }

        public virtual ICollection<RefreshToken>? RefreshTokens { get; set; }

        public virtual ICollection<Invite>? InvitesIncoming { get; set; }

        public virtual ICollection<Invite>? InvitesOutcoming { get; set; }
    }
}
