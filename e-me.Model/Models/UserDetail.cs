using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace e_me.Model.Models
{
    [DataContract]
    public class UserDetail : BaseModel
    {
        [Required]
        public Guid UserId { get; set; }

        [DataMember(Name = "CLIENT.BIRTHDAY")]
        public int BirthDay { get; set; }

        [DataMember(Name = "CLIENT.BIRTHMONTH")]
        public int BirthMonth { get; set; }

        [DataMember(Name = "CLIENT.BIRTHYEAR")]
        public int BirthYear { get; set; }

        [MaxLength(50)]
        [DataMember(Name = "CLIENT.BIRTHCITY")]
        public string BirthCity { get; set; }

        [DataMember(Name = "CLIENT.BIRTHCOUNTY")]
        public string BirthCounty { get; set; }

        [DataMember(Name = "CLIENT.BIRTHCOUNTRY")]
        public string BirthCountry { get; set; }

        [Phone]
        [DataMember(Name = "CLIENT.PHONE")]
        public string PhoneNumber { get; set; }

        [DataMember(Name = "CLIENT.HOMESTREET")]
        public string HomeStreet { get; set; }

        [DataMember(Name = "CLIENT.HOMECITY")]
        public string HomeCity { get; set; }

        [DataMember(Name = "CLIENT.HOMECOUNTY")]
        public string HomeCounty { get; set; }

        [DataMember(Name = "CLIENT.HOMECOUNTRY")]
        public string HomeCountry { get; set; }

        [DataMember(Name = "CLIENT.HOMESTREETNUMBER")]
        public string HomeStreetNumber { get; set; }

        [DataMember(Name = "CLIENT.HOMEENTRANCE")]
        public string HomeEntrance { get; set; }

        [DataMember(Name = "CLIENT.HOMEAPARTMENTNUMBER")]
        public string HomeApartmentNumber { get; set; }

        [DataMember(Name = "CLIENT.HOMEBLOCKNUMBER")]
        public string HomeBlockNumber { get; set; }

        [DataMember(Name = "CLIENT.HOMEPOSTALCODE")]
        public string HomePostalCode { get; set; }

        [MaxLength(15)]
        [DataMember(Name = "CLIENT.PERSONALNUMERICCODE")]
        public string PersonalNumericCode { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
