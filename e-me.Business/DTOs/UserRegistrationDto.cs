using System.ComponentModel.DataAnnotations;

namespace e_me.Business.DTOs
{
    public class UserRegistrationDto
    {
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(13)]
        public string PersonalNumericCode { get; set; }

        [Required]
        [MaxLength(200)]
        public string Password { get; set; }

        [Required]
        [MaxLength(200)]
        public string ConfirmPassword { get; set; }
    }
}
