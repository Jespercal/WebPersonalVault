using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace OnlineVault.Server.Models
{
    public class EncryptionUserInfo
    {
        public EncryptionUserInfo() { }

        public EncryptionUserInfo( string user_id )
        {
            UserId = user_id;
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public string? PrivateKey { get; set; }
        public string? PublicKey { get; set; }
        public string? UserPublicKey { get; set; }
        public string? ServiceXML { get; set; }

        public string SessionToken { get; set; }

        public virtual ApplicationUser User { get; set; }

        public void GenerateFirstKeys()
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048);
            PrivateKey = rsa.ExportRSAPrivateKeyPem();
            PublicKey = rsa.ExportRSAPublicKeyPem();
            ServiceXML = rsa.ToXmlString(true);
            rsa.PersistKeyInCsp = false;
        }
    }
}
