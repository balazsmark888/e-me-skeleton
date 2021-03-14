using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_me.Model.Models
{
    public class ResetPasswordToken : BaseModel
    {
        [Required]
        public Guid UserId { get; set; }

        [MaxLength(400)]
        [Required]
        public string TokenString { get; set; }

        [Required]
        public DateTime ValidTo { get; set; }

        [Required]
        public bool Expired { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
