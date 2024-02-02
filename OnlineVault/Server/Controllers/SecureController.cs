using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineVault.Server.Data;
using OnlineVault.Server.Models;
using OnlineVault.Server.Services;
using OnlineVault.Shared;
using OnlineVault.Shared.Utils;
using System.Security.Claims;
using System.Security.Cryptography.Xml;

namespace OnlineVault.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SecureController : ControllerBase
    {
        private readonly ILogger<SecureController> _logger;
        private readonly EncryptingService _es;
        private readonly ApplicationDbContext _context;

        public SecureController(ILogger<SecureController> logger, EncryptingService es, ApplicationDbContext context )
        {
            _context = context;
            _es = es;
            _logger = logger;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            string user_id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (user_id == null)
                throw new Exception("Not authorized");
            
            var infos = _context.EncryptedUserInfos
                .AsQueryable()
                .Where(e => e.UserId == user_id)
                .ToList();

            if (infos.Count > 0)
            {
                _context.EncryptedUserInfos.RemoveRange(infos);
                await _context.SaveChangesAsync();
            }

            var newInfo = new EncryptionUserInfo(user_id!);
            newInfo.GenerateFirstKeys();

            _context.EncryptedUserInfos.Add(newInfo);
            await _context.SaveChangesAsync();

            return newInfo.PublicKey!;
        }

        [HttpPost]
        public async Task Post( DTOEncryptedObject new_encrypted )
        {
            string path = StorageHandler.GetFreeName();
            await StorageHandler.SaveBytes(new_encrypted.Data, path);

            EncryptedObject new_object = new EncryptedObject()
            {
                EncryptionType = new_encrypted.EncryptionType,
                Name = new_encrypted.Name,
                Type = new_encrypted.Type,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                createdAt = DateTime.UtcNow,
                Key1 = new_encrypted.Key1,
                Key2 = new_encrypted.Key2,
                Path = path,
                IsFile = new_encrypted.Type != ""
            };

            _context.EncryptedObjects.Add(new_object);
            await _context.SaveChangesAsync();
        }
    }
}
