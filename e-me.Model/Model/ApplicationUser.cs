using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace e_me.Model.Model
{
    [Table("ApplicationUser", Schema = "dbo")]
    public class ApplicationUser : IdentityUser
    {
        
    }
}
