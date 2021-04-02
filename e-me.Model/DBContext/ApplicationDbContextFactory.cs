using e_me.Core.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DataEncryption;
using Microsoft.Extensions.Configuration;

namespace e_me.Model.DBContext
{
    public class ApplicationDbContextFactory : IApplicationDbContextFactory
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationUserContext _applicationUserContext;
        private readonly IEncryptionProvider _encryptionProvider;

        public ApplicationDbContextFactory(IConfiguration configuration, ApplicationUserContext applicationUserContext, IEncryptionProvider encryptionProvider)
        {
            _configuration = configuration;
            _applicationUserContext = applicationUserContext;
            _encryptionProvider = encryptionProvider;
        }

        public ApplicationDbContext Create()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = GetConnectionString();
            optionsBuilder.UseSqlServer(connectionString);

            return new ApplicationDbContext(optionsBuilder.Options, _encryptionProvider);
        }

        private string GetConnectionString()
        {
            var connectionString = _applicationUserContext.ConnectionString;
            return !string.IsNullOrWhiteSpace(connectionString)
                ? connectionString
                : _configuration.GetConnectionString("DbConnection");
        }
    }
}
