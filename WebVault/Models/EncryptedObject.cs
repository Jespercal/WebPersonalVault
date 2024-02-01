using Microsoft.AspNetCore.Identity;

namespace WebVault.Models
{
    public class EncryptedObject
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int EncryptionType { get; set; }

        public string Path { get; set; }
        public string Type { get; set; }

        public string? Key1 { get; set; }
        public string? Key2 { get; set; }

        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }

        public DateTime createdAt { get; set; } = DateTime.UtcNow;
    }
}
