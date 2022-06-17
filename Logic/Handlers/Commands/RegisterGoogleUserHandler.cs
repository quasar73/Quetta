using Common.Models;
using Common.Models.Commands;
using Data.Models;
using Logic.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace Logic.Handlers.Commands
{
    public class RegisterGoogleUserHandler : IRequestHandler<RegisterGoogleUserCommand, TokenModel>
    {
        private readonly UserManager<User> userManager;
        private readonly IConfiguration configuration;
        private readonly ITokenGenerator tokenGenerator;

        public RegisterGoogleUserHandler(UserManager<User> userManager, IConfiguration configuration, ITokenGenerator tokenGenerator)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.tokenGenerator = tokenGenerator;
        }

        public async Task<TokenModel> Handle(RegisterGoogleUserCommand request, CancellationToken cancellationToken)
        {
            var clientId = configuration["Authentication:Google:ClientId"];

            Payload payload = await ValidateAsync(request.IdToken, new ValidationSettings
            {
                Audience = new[] { clientId }
            });

            var user = new User
            {
                UserName = request.Username,
                FirstName = request.FirstName,
                LastName = request.LastName
            };
            await userManager.CreateAsync(user);

            var info = new UserLoginInfo(RegisterGoogleUserCommand.PROVIDER, payload.Subject, RegisterGoogleUserCommand.PROVIDER.ToUpperInvariant());
            var result = await userManager.AddLoginAsync(user, info);

            if (result.Succeeded)
            {
                return await tokenGenerator.GetToken(user);
            }

            throw new NotImplementedException();
        }
    }
}
