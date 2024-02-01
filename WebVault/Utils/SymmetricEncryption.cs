using System.Security.Cryptography;

namespace WebVault.Utils
{
    public class SymmetricEncryption
    {
        public byte[] Encrypt(byte[] dataToEncrypt, byte[] key, byte[] iv)
        {
            using Aes aes = Aes.Create();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            aes.Key = key;

            return aes.EncryptCbc(dataToEncrypt, iv);
        }

        public byte[] Decrypt(byte[] dataToDecrypt, byte[] key, byte[] iv)
        {
            using Aes aes = Aes.Create();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            aes.Key = key;

            return aes.DecryptCbc(dataToDecrypt, iv);
        }
    }
}
