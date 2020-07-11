using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace e_me.Model.Model
{
    [Table("User", Schema = "dbo")]
    public class User : IdentityUser
    {
    }
}
