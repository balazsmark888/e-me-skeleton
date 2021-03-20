using System;
using e_me.Core;
using e_me.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace e_me.Model.DBContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ClientEcdhKeyPair> ClientEcdhKeyPairs { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<JwtToken> JwtTokens { get; set; }

        public DbSet<ResetPasswordToken> ResetPasswordTokens { get; set; }

        public DbSet<SecurityRole> SecurityRoles { get; set; }

        public DbSet<UserSecurityRole> UserSecurityRoles { get; set; }

        public DbSet<ApplicationSetting> ApplicationSettings { get; set; }

        public DbSet<UserAvatar> UserAvatars { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.RemovePluralizingTableNameConvention();

            modelBuilder.Entity<User>(e =>
            {
                e.HasIndex(p => p.Email)
                    .IsUnique();
                e.HasIndex(p => p.PersonalNumericCode)
                    .IsUnique();
                e.HasIndex(p => p.LoginName)
                    .IsUnique();
            });

            modelBuilder.Entity<SecurityRole>().HasData(
                new SecurityRole
                {
                    Id = Guid.NewGuid(),
                    Name = "Administrator",
                    SecurityType = (int) Enums.SecurityType.AppAdministrator
                },
                new SecurityRole
                {
                    Id = Guid.NewGuid(),
                    Name = "Regular User",
                    SecurityType = (int) Enums.SecurityType.RegularUser
                }
            );
        }
    }
}
