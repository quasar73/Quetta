using System.ComponentModel.DataAnnotations;

namespace Common.DTO
{
    public class RegisterGoogleUserDto
    {
        public const string PROVIDER = "google";

        [Required]
        public string? IdToken { get; set; }

        [Required]
        public string? Username { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }
    }
}
