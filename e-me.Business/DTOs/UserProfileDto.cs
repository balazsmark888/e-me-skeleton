using System;
using System.ComponentModel.DataAnnotations;

namespace e_me.Business.DTOs
{
    public class UserProfileDto
    {
        public Guid UserId { get; set; }

        [Required]
        public string LoginName { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public SecurityRoleDto SecurityRoleDto { get; set; }
    }
}
