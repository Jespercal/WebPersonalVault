using System.ComponentModel.DataAnnotations;

namespace OnlineVault.Shared
{
    public class DTOEncryptedObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int EncryptionType { get; set; }

        public byte[] Data { get; set; }
        public bool IsFile { get; set; }

        public string Type { get; set; }

        public string? Key1 { get; set; }
        public string? Key2 { get; set; }

        public DateTime createdAt { get; set; }
    }
}
