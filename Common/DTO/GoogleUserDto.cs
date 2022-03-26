using System.ComponentModel.DataAnnotations;

namespace Common.DTO
{
    public class GoogleUserDto
    {
        public const string PROVIDER = "google";

        [Required]
        public string? IdToken { get; set; }
    }
}
