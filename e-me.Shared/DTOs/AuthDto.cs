using System;
using System.ComponentModel.DataAnnotations;

namespace e_me.Shared.DTOs
{
    [Serializable]
    public class AuthDto
    {
        [Required]
        public string LoginName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string PublicKey { get; set; }
    }
}
