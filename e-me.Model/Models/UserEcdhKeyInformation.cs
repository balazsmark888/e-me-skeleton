using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_me.Model.Models
{
    public class UserEcdhKeyInformation : BaseModel
    {
        [Required]
        public Guid UserId { get; set; }

        [Encrypted]
        public string SharedKey { get; set; }

        [Encrypted]
        public string IV { get; set; }

        [Encrypted]
        public string HmacKey { get; set; }

        [Encrypted]
        public string DerivedHmacKey { get; set; }

        [Encrypted]
        public string ServerPublicKey { get; set; }

        [Encrypted]
        public string ClientPublicKey { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
