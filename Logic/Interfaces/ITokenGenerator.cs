using Data.Models;

namespace Logic.Interfaces
{
    public interface ITokenGenerator
    {
        public string GenerateAccessToken(User user);
        public RefreshToken GenerateRefreshToken(User user);
    }
}
