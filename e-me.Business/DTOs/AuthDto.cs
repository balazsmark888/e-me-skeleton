using System.ComponentModel.DataAnnotations;

namespace e_me.Business.DTOs
{
    public class AuthDto
    {
        [Required]
        public string LoginName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public byte[] PublicKey { get; set; }
    }
}
