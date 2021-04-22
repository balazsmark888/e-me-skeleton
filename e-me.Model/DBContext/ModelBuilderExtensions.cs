using System;
using e_me.Core;
using e_me.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace e_me.Model.DBContext
{
    public static class ModelBuilderExtensions
    {
        public static void RemovePluralizingTableNameConvention(this ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.DisplayName());
            }
        }

        public static void AddUniqueIndexes(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DocumentTemplate>(e =>
            {
                e.HasIndex(p => p.DocumentTypeId)
                    .IsUnique();
            });

            modelBuilder.Entity<UserDetail>(e =>
            {
                e.HasIndex(p => p.PersonalNumericCode)
                    .IsUnique();
                e.HasIndex(p => p.UserId)
                    .IsUnique();
            });

            modelBuilder.Entity<User>(e =>
            {
                e.HasIndex(p => p.Email)
                    .IsUnique();
                e.HasIndex(p => p.LoginName)
                    .IsUnique();
            });
        }

        public static void InsertDefaultValues(this ModelBuilder modelBuilder)
        {
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
            modelBuilder.Entity<DocumentType>().HasData(
                new DocumentType
                {
                    Id = Guid.NewGuid(),
                    DisplayName = "Test Document Type",
                    Name = "TestDocumentType"
                },
                new DocumentType
                {
                    Id = Guid.NewGuid(),
                    DisplayName = "Self Declaration",
                    Name = "SelfDeclaration"
                });
        }
    }
}