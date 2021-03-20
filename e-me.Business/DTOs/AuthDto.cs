using System.ComponentModel.DataAnnotations;

namespace e_me.Business.DTOs
{
    public class AuthDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
