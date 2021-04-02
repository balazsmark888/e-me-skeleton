using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_me.Model.Models
{
    [Serializable]
    public class JwtToken : Model
    {
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(250)]
        public string Token { get; set; }

        public DateTime ValidTo { get; set; }

        public bool Cancelled { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
