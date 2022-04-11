using Common.DTO;
using Common.Exceptions;
using Data;
using Data.Models;
using Logic.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace Logic.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> userManager;
        private readonly IConfiguration configuration;
        private readonly ITokenGenerator tokenGenerator;
        private readonly QuettaDbContext dbContext;

        public AuthService(UserManager<User> userManager, IConfiguration configuration, ITokenGenerator tokenGenerator, QuettaDbContext dbContext)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.tokenGenerator = tokenGenerator;
            this.dbContext = dbContext;
        }

        public async Task<TokenDto?> AuthenticateGoogleUserAsync(GoogleUserDto googleUser)
        {
            var clientId = configuration["Authentication:Google:ClientId"];

            Payload payload = await ValidateAsync(googleUser.IdToken, new ValidationSettings
            {
                Audience = new[] { clientId }
            });

            var user = await GetExternalLoginUserAsync(GoogleUserDto.PROVIDER, payload.Subject);
            
            return user == null ? null : await GetToken(user);
        }

        public async Task<TokenDto> RegisterGoogleUserAsync(RegisterGoogleUserDto registerGoogleUser)
        {
            var clientId = configuration["Authentication:Google:ClientId"];

            Payload payload = await ValidateAsync(registerGoogleUser.IdToken, new ValidationSettings
            {
                Audience = new[] { clientId }
            });

            var user = new User
            {
                UserName = registerGoogleUser.Username,
                FirstName = registerGoogleUser.FirstName,
                LastName = registerGoogleUser.LastName
            };
            await userManager.CreateAsync(user);

            var info = new UserLoginInfo(RegisterGoogleUserDto.PROVIDER, payload.Subject, RegisterGoogleUserDto.PROVIDER.ToUpperInvariant());
            var result = await userManager.AddLoginAsync(user, info);

            if (result.Succeeded)
            {
                return await GetToken(user);
            }

            throw new NotImplementedException();
        }

        public async Task<bool> CheckOutUsername(string username)
        {
            var user = await userManager.FindByNameAsync(username);

            return user != null;
        }

        public async Task<TokenDto> RefreshToken(string refreshToken)
        {
            var token = dbContext.RefreshTokens.FirstOrDefault(rt => rt.Token == refreshToken);

            if (token == null)
            {
                throw new InvalidTokenException();
            }

            if (token.Expires < DateTime.UtcNow)
            {
                throw new TokenExpiredException();
            }

            var user = await userManager.FindByIdAsync(token.UserId);

            if (user == null)
            {
                throw new InvalidTokenException();
            }

            dbContext.RefreshTokens.Remove(token);
            await dbContext.SaveChangesAsync();

            return await GetToken(user);
        }

        private async Task<User?> GetExternalLoginUserAsync(string provider, string key)
        {
            var user = await userManager.FindByLoginAsync(provider, key);
            return user;
        }

        private async Task<TokenDto> GetToken(User user)
        {
            var accessToken = tokenGenerator.GenerateAccessToken(user);
            var refreshToken = tokenGenerator.GenerateRefreshToken(user);

            if (refreshToken != null)
            {
                dbContext.RefreshTokens.Add(refreshToken);
                await dbContext.SaveChangesAsync();
            }

            return new TokenDto
            {
                AccessToken = accessToken ?? "",
                RefreshToken = refreshToken?.Token ?? ""
            };
        }
    }
}
