namespace Quetta.Logic.Interfaces
{
    public interface IBaseEncryptingService
    {
        public Task<string> Encrypt(string text);
        public Task<string?> Decrypt(string text);
    }
}
