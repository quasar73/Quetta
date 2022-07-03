using MediatR;

namespace Common.Models.Commands
{
    public record class RegisterGoogleUserCommand : IRequest<TokenModel>
    {
        public const string PROVIDER = "google";

        public string IdToken { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
