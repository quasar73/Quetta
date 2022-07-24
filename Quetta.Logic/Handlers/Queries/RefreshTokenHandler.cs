using Quetta.Common.Exceptions;
using Quetta.Common.Models;
using Quetta.Common.Models.Queries;
using Data;
using Quetta.Data.Models;
using Quetta.Logic.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Quetta.Logic.Handlers.Queries
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenQuery, TokenModel>
    {
        private readonly UserManager<User> userManager;
        private readonly QuettaDbContext dbContext;
        private readonly ITokenGenerator tokenGenerator;

        public RefreshTokenHandler(UserManager<User> userManager, QuettaDbContext dbContext, ITokenGenerator tokenGenerator)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
            this.tokenGenerator = tokenGenerator;
        }

        public async Task<TokenModel> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
        {
            var token = dbContext.RefreshTokens.FirstOrDefault(rt => rt.Token == request.RefreshToken);

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

            return await tokenGenerator.GetToken(user);
        }
    }
}
