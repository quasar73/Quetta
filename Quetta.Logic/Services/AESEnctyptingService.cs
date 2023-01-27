using Microsoft.Extensions.Configuration;
using Quetta.Logic.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Quetta.Logic.Services
{
    public class AESEnctyptingService : IBaseEncryptingService
    {
        private readonly IConfiguration configuration;

        private byte[] Salt { get; set; }
        private string ActualSecret { get; set; }

        public AESEnctyptingService(IConfiguration configuration)
        {
            this.configuration = configuration;

            Salt = Encoding.ASCII.GetBytes(configuration["Crypt:Salt"]);
            var actualSecretName = configuration["Crypt:ActualSecret"];
            ActualSecret = configuration.GetValue<string>($"Crypt:Secrets:{actualSecretName}");
        }

        public async Task<string> Encrypt(string text)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(ActualSecret))
            {
                throw new ArgumentNullException();
            }

            string enctyptedText = null;

            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(ActualSecret, Salt);

            using (var aes = Aes.Create())
            {
                aes.Key = key.GetBytes(aes.KeySize / 8);

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    msEncrypt.Write(BitConverter.GetBytes(aes.IV.Length), 0, sizeof(int));
                    msEncrypt.Write(aes.IV, 0, aes.IV.Length);
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(text);
                        }
                    }
                    enctyptedText = Convert.ToBase64String(msEncrypt.ToArray());
                }
            }

            return enctyptedText;
        }

        public async Task<string?> Decrypt(string text, string secretVersion)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(ActualSecret))
            {
                throw new ArgumentNullException();
            }

            string dectyptedText = null;

            try
            {
                var secret = configuration.GetValue<string>($"Crypt:Secrets:{secretVersion}");
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(secret, Salt);

                byte[] bytes = Convert.FromBase64String(text);
                using (MemoryStream msDecrypt = new MemoryStream(bytes))
                {
                    using (var aes = Aes.Create())
                    {
                        aes.Key = key.GetBytes(aes.KeySize / 8);
                        aes.IV = ReadByteArray(msDecrypt);
                        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                dectyptedText = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch
            {
                return null;
            }

            return dectyptedText;
        }

        private static byte[] ReadByteArray(Stream stream)
        {
            byte[] rawLength = new byte[sizeof(int)];
            if (stream.Read(rawLength, 0, rawLength.Length) != rawLength.Length)
            {
                throw new SystemException("Stream did not contain properly formatted byte array");
            }

            byte[] buffer = new byte[BitConverter.ToInt32(rawLength, 0)];
            if (stream.Read(buffer, 0, buffer.Length) != buffer.Length)
            {
                throw new SystemException("Did not read byte array properly");
            }

            return buffer;
        }
    }
}
