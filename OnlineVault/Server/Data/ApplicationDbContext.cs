﻿using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OnlineVault.Server.Models;

namespace OnlineVault.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public DbSet<EncryptedObject> EncryptedObjects { get; set; }
        public DbSet<EncryptionUserInfo> EncryptedUserInfos { get; set; }

        public ApplicationDbContext(DbContextOptions options,IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
    }
}
