using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Common.Models.Commands
{
    public record class RegisterGoogleUserCommand : IRequest<TokenModel>
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
