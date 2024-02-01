namespace OnlineVault.Shared
{
    public class EncryptionSettings
    {
        public string name { get; set; }

        public EncryptionType type { get; set; }
        public string? key { get; set; }
        public string? key2 { get; set; }
        public Byte[] data { get; set; }
        public string filename { get; set; }
        public string user_id { get; set; }
    }
}

namespace OnlineVault.Shared
{
    public enum EncryptionType
    {
        Hash = 1,
        Symmetric = 2,
        Asymmetric = 3
    }
}
