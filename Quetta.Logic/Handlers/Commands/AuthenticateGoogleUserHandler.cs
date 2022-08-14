using Quetta.Common.Models;
using Quetta.Common.Models.Commands;
using Quetta.Data.Models;
using Quetta.Logic.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace Quetta.Logic.Handlers.Commands
{
    public class AuthenticateGoogleUserHandler : IRequestHandler<AuthenticateGoogleUserCommand, TokenModel?>
    {
        private readonly UserManager<User> userManager;
        private readonly IConfiguration configuration;
        private readonly ITokenGenerator tokenGenerator;

        public AuthenticateGoogleUserHandler(IConfiguration configuration, UserManager<User> userManager, ITokenGenerator tokenGenerator)
        {
            this.configuration = configuration;
            this.userManager = userManager;
            this.tokenGenerator = tokenGenerator;
        }

        public async Task<TokenModel?> Handle(AuthenticateGoogleUserCommand request, CancellationToken cancellationToken)
        {
            var clientId = configuration["Authentication:Google:ClientId"];

            Payload payload = await ValidateAsync(request.IdToken, new ValidationSettings
            {
                Audience = new[] { clientId }
            });
            
            var user = await userManager.FindByLoginAsync(AuthenticateGoogleUserCommand.PROVIDER, payload.Subject);

            return user == null ? null : await tokenGenerator.GetToken(user);
        }
    }
}
