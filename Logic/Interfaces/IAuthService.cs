using Common.DTO;

namespace Logic.Interfaces
{
    public interface IAuthService
    {
        public Task<TokenDto?> AuthenticateGoogleUserAsync(GoogleUserDto googleUser);
        public Task<TokenDto> RegisterGoogleUserAsync(RegisterGoogleUserDto googleUser);
        public Task<TokenDto> RefreshToken(string refreshToken);
        public Task<bool> CheckOutUsername(string username);
    }
}
