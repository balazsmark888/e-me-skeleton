using System.ComponentModel.DataAnnotations;

namespace e_me.Model.Models
{
    public class ClientECDHKeyPair : BaseModel
    {
        [Required]
        [MaxLength(200)]
        public string PublicKey { get; set; }

        [Required]
        [MaxLength(200)]
        public string DerivedKey { get; set; }

        [Required]
        [MaxLength(200)]
        public string SessionId { get; set; }
    }
}
