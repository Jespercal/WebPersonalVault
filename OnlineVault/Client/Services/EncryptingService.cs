using OnlineVault.Shared;
using OnlineVault.Shared.Utils;
using System.Security.Cryptography;
using System.Text;

namespace OnlineVault.Client.Services
{
    public class EncryptingService
    {
        public async Task<DTOEncryptionResult> Encrypt( EncryptionSettings settings )
        {
            if(settings.type == EncryptionType.Hash)
                return await EncryptHash(settings);
            else if(settings.type == EncryptionType.Symmetric)
                return await EncryptSymmetric(settings);
            else if(settings.type == EncryptionType.Asymmetric)
                return await EncryptAsymmetric(settings);

            return new DTOEncryptionResult("No such encryption is available");
        }

        private async Task<DTOEncryptionResult> EncryptHash( EncryptionSettings settings )
        {
            byte[] salt = null;
            if (settings.key != null)
                salt = Encoding.ASCII.GetBytes(settings.key);

            byte[] encryptedData = HashEncryption.Encrypt(settings.data, salt);

            DTOEncryptedObject obj = new DTOEncryptedObject()
            {
                Data = encryptedData,
                Name = !string.IsNullOrEmpty(settings.name) ? settings.name : "Unknown",
                Key1 = salt != null ? Convert.ToBase64String(salt) : null,
                EncryptionType = (int)EncryptionType.Hash,
                Type = (!string.IsNullOrEmpty(settings.filename) ? settings.filename : ""),
                IsFile = !string.IsNullOrEmpty(settings.filename)
            };

            return new DTOEncryptionResult(obj);
        }

        private async Task<DTOEncryptionResult> EncryptSymmetric(EncryptionSettings settings)
        {
            var obj = new DTOEncryptedObject()
            {
                Data = settings.data,
                Name = !string.IsNullOrEmpty(settings.name) ? settings.name : "Unknown",
                EncryptionType = (int)EncryptionType.Symmetric,
                Key1 = settings.key,
                Key2 = settings.key2,
                Type = (!string.IsNullOrEmpty(settings.filename) ? settings.filename : "")
            };

            return new DTOEncryptionResult(obj);
        }

        private async Task<DTOEncryptionResult> EncryptAsymmetric(EncryptionSettings settings)
        {
            return new();
        }
    }

    public class DTOEncryptionResult
    {
        public DTOEncryptionResult() { }
        public DTOEncryptionResult(DTOEncryptedObject data)
        {
            Data = data;
        }
        public DTOEncryptionResult(string message)
        {
            Data = null;
            Failed = true;
            Message = message;
        }

        public DTOEncryptedObject? Data { get; set; }
        public bool Failed { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
