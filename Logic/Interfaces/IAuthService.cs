using Common.DTO;

namespace Logic.Interfaces
{
    public interface IAuthService
    {
        public Task<string?> AuthenticateGoogleUserAsync(GoogleUserDto googleUser);
    }
}
