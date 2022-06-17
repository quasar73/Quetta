﻿using Common.Models;
using Data;
using Data.Models;
using Logic.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Logic.Services
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration configuration;
        private readonly QuettaDbContext dbContext;

        public TokenGenerator(IConfiguration configuration, QuettaDbContext dbContext)
        {
            this.configuration = configuration;
            this.dbContext = dbContext;
        }

        public string GenerateAccessToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Authentication:Jwt:Secret"]);

            var expires = DateTime.UtcNow.AddDays(1);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim(ClaimTypes.Surname, user?.LastName ?? ""),
                    new Claim(ClaimTypes.GivenName, user?.FirstName ?? ""),
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

        public RefreshToken GenerateRefreshToken(User user)
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            var token = Convert.ToBase64String(randomNumber);

            var refreshToken = new RefreshToken
            {
                Id = Guid.NewGuid().ToString(),
                Token = token,
                Expires = DateTime.UtcNow.AddDays(30),
                UserId = user.Id,
                User = user
            };

            return refreshToken;
        }

        public async Task<TokenModel> GetToken(User user)
        {
            var accessToken = GenerateAccessToken(user);
            var refreshToken = GenerateRefreshToken(user);

            if (refreshToken != null)
            {
                dbContext.RefreshTokens.Add(refreshToken);
                await dbContext.SaveChangesAsync();
            }

            return new TokenModel
            {
                AccessToken = accessToken ?? "",
                RefreshToken = refreshToken?.Token ?? ""
            };
        }
    }
}
