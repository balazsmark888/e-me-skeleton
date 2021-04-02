using System;
using e_me.Core;
using e_me.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DataEncryption;
using Microsoft.EntityFrameworkCore.DataEncryption.Providers;

namespace e_me.Model.DBContext
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IEncryptionProvider _encryptionProvider;

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IEncryptionProvider encryptionProvider)
            : base(options)
        {
            _encryptionProvider = encryptionProvider;
        }

        public DbSet<UserEcdhKeyInformation> ClientEcdhKeyPairs { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<JwtToken> JwtTokens { get; set; }

        public DbSet<SecurityRole> SecurityRoles { get; set; }

        public DbSet<UserSecurityRole> UserSecurityRoles { get; set; }

        public DbSet<ApplicationSetting> ApplicationSettings { get; set; }

        public DbSet<UserAvatar> UserAvatars { get; set; }

        public DbSet<UserDetail> UserDetails { get; set; }

        public DbSet<DocumentType> DocumentTypes { get; set; }

        public DbSet<UserEcdhKeyInformation> UserEcdhKeyInformationSet { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseEncryption(_encryptionProvider);

            modelBuilder.RemovePluralizingTableNameConvention();

            modelBuilder.Entity<User>(e =>
            {
                e.HasIndex(p => p.Email)
                    .IsUnique();
                e.HasIndex(p => p.LoginName)
                    .IsUnique();
            });

            modelBuilder.Entity<UserDetail>(e =>
            {
                e.HasIndex(p => p.PersonalNumericCode)
                    .IsUnique();
            });

            modelBuilder.Entity<SecurityRole>().HasData(
                new SecurityRole
                {
                    Id = Guid.NewGuid(),
                    Name = "Administrator",
                    SecurityType = (int)Enums.SecurityType.AppAdministrator
                },
                new SecurityRole
                {
                    Id = Guid.NewGuid(),
                    Name = "Regular User",
                    SecurityType = (int)Enums.SecurityType.RegularUser
                }
            );
        }
    }
}
