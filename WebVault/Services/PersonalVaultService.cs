using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WebVault.Data;
using WebVault.Models;

namespace WebVault.Services
{
    public class PersonalVaultService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _dbContext;
        public PersonalVaultService( SignInManager<IdentityUser> signInManager, IHttpContextAccessor context, ApplicationDbContext dbContext )
        {
            _dbContext = dbContext;
            _signInManager = signInManager;
            if(_signInManager.IsSignedIn(context.HttpContext.User))
            {
                UserId = context.HttpContext.User.Claims.First().Value;
                Load();
            }
        }

        public string UserId { get; set; }
        public string PersonalKey { get; set; }
        public List<EncryptedObject> Objects { get; set; }

        private void Load()
        {
            Objects = _dbContext.EncryptedObjects
                .AsQueryable()
                .Where(e => e.UserId == UserId)
                .ToList();
        }
    }
}
