using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using OnlineVault.Server.Data;
using OnlineVault.Server.Models;

namespace OnlineVault.Server.Services
{
    public class PersonalVaultService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _dbContext;
        public PersonalVaultService( SignInManager<ApplicationUser> signInManager, IHttpContextAccessor context, ApplicationDbContext dbContext )
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

        public async Task LoadAsync()
        {
            Objects = await _dbContext.EncryptedObjects
                .AsQueryable()
                .Where(e => e.UserId == UserId)
                .ToListAsync();
        }
        public void Load()
        {
            Objects = _dbContext.EncryptedObjects
                .AsQueryable()
                .Where(e => e.UserId == UserId)
                .ToList();
        }
    }
}
