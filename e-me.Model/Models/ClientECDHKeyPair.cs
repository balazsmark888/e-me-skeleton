using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_me.Model.Models
{
    [Table("ClientEcdhKeyPair", Schema = "dbo")]
    [Serializable]
    public class ClientEcdhKeyPair : BaseModel
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
