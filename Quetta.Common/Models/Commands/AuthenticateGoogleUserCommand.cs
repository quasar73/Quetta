using MediatR;

namespace Quetta.Common.Models.Commands
{
    public record class AuthenticateGoogleUserCommand : IRequest<TokenModel?>
    {
        public const string PROVIDER = "google";

        public string IdToken { get; set; }
    }
}
