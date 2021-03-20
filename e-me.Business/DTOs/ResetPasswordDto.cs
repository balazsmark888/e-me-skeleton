using System.ComponentModel.DataAnnotations;

namespace e_me.Business.DTOs
{
    public class ResetPasswordDto
    {
        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string ConfirmPassword { get; set; }
    }
}
