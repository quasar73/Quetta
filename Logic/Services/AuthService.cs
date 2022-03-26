using Common.DTO;
using Data.Models;
using Logic.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace Logic.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> userManager;
        private readonly IConfiguration configuration;

        public AuthService(UserManager<User> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        public async Task<string?> AuthenticateGoogleUserAsync(GoogleUserDto googleUser)
        {
            var clientId = configuration["Authentication:Google:ClientId"];

            Payload payload = await ValidateAsync(googleUser.IdToken, new ValidationSettings
            {
                Audience = new[] { clientId }
            });

            var user = await GetExternalLoginUser(GoogleUserDto.PROVIDER, payload.Subject);

            if (user == null)
            {
                return null;
            }

            return await GenerateJwtToken(user);
        }

        private async Task<User?> GetExternalLoginUser(string provider, string key)
        {
            var user = await userManager.FindByLoginAsync(provider, key);
            return user;
        }

        private async Task<string> GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Authentication:Jwt:Secret"]);

            var expires = DateTime.UtcNow.AddDays(7);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim(ClaimTypes.Surname, user?.FirstName ?? ""),
                    new Claim(ClaimTypes.GivenName, user?.LastName ?? ""),
                    new Claim(ClaimTypes.NameIdentifier, user?.UserName ?? "")
                }),
                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = configuration["Authentication:Jwt:Issuer"],
                Audience = configuration["Authentication:Jwt:Audience"]
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return token;
        }
    }
}
