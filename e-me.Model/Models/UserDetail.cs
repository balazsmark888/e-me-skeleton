﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;

namespace e_me.Model.Models
{
    [DataContract]
    public class UserDetail : BaseModel
    {
        [Required]
        public Guid UserId { get; set; }

        [Encrypted]
        [DataMember(Name = "CLIENT_FULLNAME")]
        public string FullName { get; set; }

        [Encrypted]
        [DataMember(Name = "CLIENT_EMAIL")]
        public string Email { get; set; }

        [Encrypted]
        [DataMember(Name = "CLIENT_BIRTHDAY")]
        public string BirthDay { get; set; }

        [Encrypted]
        [DataMember(Name = "CLIENT_BIRTHMONTH")]
        public string BirthMonth { get; set; }

        [Encrypted]
        [DataMember(Name = "CLIENT_BIRTHYEAR")]
        public string BirthYear { get; set; }

        [Encrypted]
        [DataMember(Name = "CLIENT_BIRTHCITY")]
        public string BirthCity { get; set; }

        [Encrypted]
        [DataMember(Name = "CLIENT_BIRTHCOUNTY")]
        public string BirthCounty { get; set; }

        [Encrypted]
        [DataMember(Name = "CLIENT_BIRTHCOUNTRY")]
        public string BirthCountry { get; set; }

        [Encrypted]
        [DataMember(Name = "CLIENT_PHONE")]
        public string PhoneNumber { get; set; }

        [Encrypted]
        [DataMember(Name = "CLIENT_HOMESTREET")]
        public string HomeStreet { get; set; }

        [Encrypted]
        [DataMember(Name = "CLIENT_HOMECITY")]
        public string HomeCity { get; set; }

        [Encrypted]
        [DataMember(Name = "CLIENT_HOMECOUNTY")]
        public string HomeCounty { get; set; }

        [Encrypted]
        [DataMember(Name = "CLIENT_HOMECOUNTRY")]
        public string HomeCountry { get; set; }

        [Encrypted]
        [DataMember(Name = "CLIENT_HOMESTREETNUMBER")]
        public string HomeStreetNumber { get; set; }

        [Encrypted]
        [DataMember(Name = "CLIENT_HOMEENTRANCE")]
        public string HomeEntrance { get; set; }

        [Encrypted]
        [DataMember(Name = "CLIENT_HOMEAPARTMENTNUMBER")]
        public string HomeApartmentNumber { get; set; }

        [Encrypted]
        [DataMember(Name = "CLIENT_HOMEBLOCKNUMBER")]
        public string HomeBlockNumber { get; set; }

        [Encrypted]
        [DataMember(Name = "CLIENT_HOMEPOSTALCODE")]
        public string HomePostalCode { get; set; }

        [Encrypted]
        [DataMember(Name = "CLIENT_PERSONALNUMERICCODE")]
        public string PersonalNumericCode { get; set; }

        [NotMapped]
        [DataMember(Name = "CLIENT_FULLADDRESS")]
        public string FullAddress => $"{HomeStreet} {HomeStreetNumber}, {HomeBlockNumber}/{HomeEntrance}/{HomeApartmentNumber}, {HomeCity},{HomeCounty},{HomeCountry}";

        [NotMapped]
        [DataMember(Name = "CLIENT_BIRTHPLACE")]
        public string BirthPlace => $"{BirthCity},{BirthCounty},{BirthCountry}";

        [NotMapped]
        [DataMember(Name = "CLIENT_FIRSTNAME")]
        public string FirstName => FullName?.Split(' ').FirstOrDefault();

        [NotMapped]
        [DataMember(Name = "CLIENT_LASTNAME")]
        public string LastName => FullName?.Split(' ').LastOrDefault();

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
