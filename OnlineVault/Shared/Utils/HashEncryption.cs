using System.Security.Cryptography;

namespace OnlineVault.Shared.Utils
{
    public class HashEncryption
    {
        public static byte[] Encrypt(byte[] data, byte[]? salt)
        {
            using SHA256 sha256 = SHA256.Create();
            return sha256.ComputeHash(salt != null ? CommonUtils.CombineByteArrays(data, salt) : data);
        }

        public static bool AssertIfSame(byte[] data, byte[]? salt, byte[] compareWith)
        {
            using SHA256 sha256 = SHA256.Create();
            return sha256.ComputeHash(salt != null ? CommonUtils.CombineByteArrays(data, salt) : data) == compareWith;
        }
    }
}
