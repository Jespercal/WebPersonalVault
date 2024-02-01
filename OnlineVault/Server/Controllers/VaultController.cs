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
    public class VaultController : ControllerBase
    {
        private readonly ILogger<VaultController> _logger;
        private readonly EncryptingService _es;
        private readonly ApplicationDbContext _context;

        public VaultController(ILogger<VaultController> logger, EncryptingService es, ApplicationDbContext context )
        {
            _context = context;
            _es = es;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<DTOEncryptedObject> Get()
        {
            string user_id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _context.EncryptedObjects
                .AsQueryable()
                .Where(e => e.UserId == user_id)
                .OrderByDescending(e => e.createdAt)
                .Select(e => e.ToDTO())
                .ToList();
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
