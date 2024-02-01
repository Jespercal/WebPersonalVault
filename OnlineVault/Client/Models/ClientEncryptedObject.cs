using System.ComponentModel.DataAnnotations;

namespace OnlineVault.Shared
{
    public class ClientEncryptedObject : DTOEncryptedObject
    {
        public ClientEncryptedObject()
        {
            DataDecrypted = null;
            IsDataDecrypted = false;
        }
        public ClientEncryptedObject( DTOEncryptedObject dto )
        {
            Id = dto.Id;
            Name = dto.Name;
            EncryptionType = dto.EncryptionType;
            createdAt = dto.createdAt;
            Data = dto.Data;
            Key1 = dto.Key1;
            Key2 = dto.Key2;
            Type = dto.Type;
            DataDecrypted = null;
            IsDataDecrypted = false;
            IsFile = dto.IsFile;
        }

        public virtual byte[]? DataDecrypted { get; set; }
        public bool IsDataDecrypted { get; set; }
    }
}
