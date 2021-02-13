using e_me.Core.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace e_me.Model.DBContext
{
    public class ApplicationDbContextFactory : IApplicationDbContextFactory
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationUserContext _applicationUserContext;


        public ApplicationDbContextFactory(IConfiguration configuration, ApplicationUserContext applicationUserContext)
        {
            _applicationUserContext = applicationUserContext;
            _configuration = configuration;
        }

        public DbContext GetDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = GetConnectionString();

            optionsBuilder.UseSqlServer(connectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
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
