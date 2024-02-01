using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using WebVault.Data;
using WebVault.Models;
using WebVault.Utils;

namespace WebVault.Services
{
    public class EncryptingService : IDisposable
    {
        private readonly ApplicationDbContext _context;
        public EncryptingService( ApplicationDbContext provider )
        {
            _context = provider;
        }

        public async Task<string> Encrypt( EncryptionSettings settings )
        {
            if(settings.type == EncryptionType.Hash)
                return await EncryptHash(settings);
            else if(settings.type == EncryptionType.Symmetric)
                return await EncryptSymmetric(settings);
            else if(settings.type == EncryptionType.Asymmetric)
                return await EncryptAsymmetric(settings);

            return "wrong_type";
        }

        private async Task<string> EncryptHash( EncryptionSettings settings )
        {
            byte[] salt;
            if (settings.key != null)
                salt = Encoding.ASCII.GetBytes(settings.key);
            else
                salt = CommonUtils.GenerateRandomByteArray();

            byte[] encryptedData = HashEncryption.Encrypt(settings.data, salt);

            string path = StorageHandler.GetFreeName();
            await StorageHandler.SaveBytes(encryptedData, path);

            var obj = new EncryptedObject()
            {
                UserId = settings.user_id,
                Name = !string.IsNullOrEmpty(settings.name) ? settings.name : "Unknown",
                Path = path,
                Key1 = Encoding.ASCII.GetString(salt),
                EncryptionType = (int)EncryptionType.Hash,
                Type = (settings?.dataInfo != null ? (settings.dataInfo.Filename + "." + settings.dataInfo.Extension) : "")
            };
            _context.EncryptedObjects.Add(obj);
            await _context.SaveChangesAsync();

            return "";
        }
        private async Task<string> EncryptSymmetric(EncryptionSettings settings)
        {
            byte[] key;
            if(settings.key != null)
                key = Encoding.ASCII.GetBytes(settings.key);
            else
                key = CommonUtils.GenerateRandomByteArray(32);

            using Aes aes = Aes.Create();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            try
            {
                aes.Key = key;
            }
            catch (Exception ex)
            {
                return "too_small";
            }

            byte[] iv = CommonUtils.GenerateRandomByteArray(16);

            byte[] encryptedData = aes.EncryptCbc(settings.data, iv);

            string path = StorageHandler.GetFreeName();
            await StorageHandler.SaveBytes(encryptedData, path);

            var obj = new EncryptedObject()
            {
                UserId = settings.user_id,
                Name = !string.IsNullOrEmpty(settings.name) ? settings.name : "Unknown",
                Path = path,
                EncryptionType = (int)EncryptionType.Symmetric,
                Key1 = Encoding.ASCII.GetString(key),
                Key2 = Encoding.ASCII.GetString(iv),
                Type = (settings?.dataInfo != null ? (settings.dataInfo.Filename + "." + settings.dataInfo.Extension) : "")
            };
            _context.EncryptedObjects.Add(obj);
            await _context.SaveChangesAsync();

            return "";
        }
        private async Task<string> EncryptAsymmetric(EncryptionSettings settings)
        {
            return "";
        }

        public void Dispose()
        {

        }
    }
}
