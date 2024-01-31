using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using WebVault.Models;

namespace WebVault.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<EncryptedObject> EncryptedObjects { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
