using e_me.Model.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace e_me.server.Mvc.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        #region Public Properties

        public DbSet<Item> Items { get; set; }

        #endregion

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
