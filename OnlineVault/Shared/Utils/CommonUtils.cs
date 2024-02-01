using System.Security.Cryptography;

namespace OnlineVault.Shared.Utils
{
    public class CommonUtils
    {
        public static byte[] GenerateRandomByteArray(int length = 32)
        {
            using RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
            byte[] randomNumber = new byte[length];
            randomNumberGenerator.GetBytes(randomNumber);

            return randomNumber;
        }
        public static byte[] CombineByteArrays(byte[] first, byte[] second)
        {
            byte[] ret = new byte[first.Length + second.Length];

            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);

            return ret;
        }
    }
}
