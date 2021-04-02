using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace e_me.Model.Models
{
    [DataContract]
    public class UserDetail : Model
    {
        [Required]
        public Guid UserId { get; set; }

        [Encrypted]
        [DataMember(Name = "CLIENT.BIRTHDAY")]
        public string BirthDay { get; set; }

        [Encrypted]
        [DataMember(Name = "CLIENT.BIRTHMONTH")]
        public string BirthMonth { get; set; }

        [Encrypted]
        [DataMember(Name = "CLIENT.BIRTHYEAR")]
        public string BirthYear { get; set; }

        [Encrypted]
        [DataMember(Name = "CLIENT.BIRTHCITY")]
        public string BirthCity { get; set; }

        [Encrypted]
        [DataMember(Name = "CLIENT.BIRTHCOUNTY")]
        public string BirthCounty { get; set; }

        [Encrypted]
        [DataMember(Name = "CLIENT.BIRTHCOUNTRY")]
        public string BirthCountry { get; set; }

        [Encrypted]
        [DataMember(Name = "CLIENT.PHONE")]
        public string PhoneNumber { get; set; }

        [Encrypted]
        [DataMember(Name = "CLIENT.HOMESTREET")]
        public string HomeStreet { get; set; }

        [Encrypted]
        [DataMember(Name = "CLIENT.HOMECITY")]
        public string HomeCity { get; set; }

        [Encrypted]
        [DataMember(Name = "CLIENT.HOMECOUNTY")]
        public string HomeCounty { get; set; }

        [Encrypted]
        [DataMember(Name = "CLIENT.HOMECOUNTRY")]
        public string HomeCountry { get; set; }

        [Encrypted]
        [DataMember(Name = "CLIENT.HOMESTREETNUMBER")]
        public string HomeStreetNumber { get; set; }

        [Encrypted]
        [DataMember(Name = "CLIENT.HOMEENTRANCE")]
        public string HomeEntrance { get; set; }

        [Encrypted]
        [DataMember(Name = "CLIENT.HOMEAPARTMENTNUMBER")]
        public string HomeApartmentNumber { get; set; }

        [Encrypted]
        [DataMember(Name = "CLIENT.HOMEBLOCKNUMBER")]
        public string HomeBlockNumber { get; set; }

        [Encrypted]
        [DataMember(Name = "CLIENT.HOMEPOSTALCODE")]
        public string HomePostalCode { get; set; }

        [Encrypted]
        [DataMember(Name = "CLIENT.PERSONALNUMERICCODE")]
        public string PersonalNumericCode { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
