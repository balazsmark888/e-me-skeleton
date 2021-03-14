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
        public Guid TenantId { get; set; }

        public string TenantName { get; set; }

        [Required]
        public string Email { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public SecurityRoleDto SecurityRoleDto { get; set; }
    }
}
