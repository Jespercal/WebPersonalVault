using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OnlineVault.Shared;
using OnlineVault.Shared.Utils;

namespace OnlineVault.Server.Models
{
    public class EncryptedObject
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int EncryptionType { get; set; }

        public bool IsFile { get; set; }

        public string Path { get; set; }
        public string Type { get; set; }

        public string? Key1 { get; set; }
        public string? Key2 { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public DateTime createdAt { get; set; } = DateTime.UtcNow;

        public DTOEncryptedObject ToDTO()
        {
            return new DTOEncryptedObject()
            {
                Id = Id,
                Name = Name,
                EncryptionType = EncryptionType,
                createdAt = createdAt,
                Data = StorageHandler.ReadBytes(Path),
                Key1 = Key1,
                Key2 = Key2,
                Type = Type,
                IsFile = IsFile,
            };
        }
    }
}
