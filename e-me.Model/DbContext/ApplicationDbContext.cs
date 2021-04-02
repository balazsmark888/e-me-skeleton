using e_me.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DataEncryption;

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

        public DbSet<DocumentTemplate> DocumentTemplates { get; set; }

        public DbSet<UserDocument> UserDocuments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseEncryption(_encryptionProvider);

            modelBuilder.RemovePluralizingTableNameConvention();

            modelBuilder.AddUniqueIndexes();

            modelBuilder.InsertDefaultValues();
        }
    }
}
