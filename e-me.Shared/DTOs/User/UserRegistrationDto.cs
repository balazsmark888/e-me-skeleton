using System;
using System.ComponentModel.DataAnnotations;

namespace e_me.Shared.DTOs.User
{
    [Serializable]
    public class UserRegistrationDto
    {
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LoginName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(200)]
        public string Password { get; set; }

        [Required]
        [MaxLength(200)]
        public string ConfirmPassword { get; set; }
    }
}
