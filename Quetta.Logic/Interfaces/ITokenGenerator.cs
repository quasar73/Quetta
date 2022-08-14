using Quetta.Common.Models;
using Quetta.Data.Models;

namespace Quetta.Logic.Interfaces
{
    public interface ITokenGenerator
    {
        public string GenerateAccessToken(User user);
        public RefreshToken GenerateRefreshToken(User user);
        public Task<TokenModel> GetToken(User user); 
    }
}
