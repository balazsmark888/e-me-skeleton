using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_me.Model.Models
{
    [Table("UserEcdhKeyInformationExtensionMethods", Schema = "dbo")]
    public class UserEcdhKeyInformation : BaseModel
    {
        public byte[] AesKey { get; set; }

        public byte[] HmacKey { get; set; }

        public byte[] DerivedHmacKey { get; set; }

        public byte[] PublicKey { get; set; }

        public byte[] ClientPublicKey { get; set; }
    }
}
