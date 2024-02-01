namespace WebVault.Models
{
    public class EncryptionSettings
    {
        public string name { get; set; }

        public EncryptionType type { get; set; }
        public string? key { get; set; }
        public Byte[] data { get; set; }
        public EncryptedFileInfo? dataInfo { get; set; }
        public string user_id { get; set; }
    }
}

namespace WebVault
{
    public enum EncryptionType
    {
        Hash = 1,
        Symmetric = 2,
        Asymmetric = 3
    }

    public class EncryptedFileInfo
    {
        public string Filename { get; set; }
        public string Extension { get; set; }
        public string Metatype { get; set; }
    }
}
