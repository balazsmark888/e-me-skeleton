using System.ComponentModel.DataAnnotations;

namespace e_me.Business.DTOs
{
    public class PasswordResetEmail
    {
        [Required]
        public string Email { get; set; }
    }
}
