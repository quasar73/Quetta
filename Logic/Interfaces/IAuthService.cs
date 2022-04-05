using Common.DTO;

namespace Logic.Interfaces
{
    public interface IAuthService
    {
        public Task<string?> AuthenticateGoogleUserAsync(GoogleUserDto googleUser);
        public Task<string?> RegisterGoogleUserAsync(RegisterGoogleUserDto googleUser);
        public Task<bool> CheckOutUsername(string username);
    }
}
