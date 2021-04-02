using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_me.Model.Models
{
    public class UserEcdhKeyInformation : BaseModel
    {
        [Required]
        public Guid UserId { get; set; }

        public byte[] AesKey { get; set; }

        public byte[] HmacKey { get; set; }

        public byte[] DerivedHmacKey { get; set; }

        public byte[] PublicKey { get; set; }

        public byte[] ClientPublicKey { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
