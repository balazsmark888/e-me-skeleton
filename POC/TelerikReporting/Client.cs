using System.Runtime.Serialization;

namespace TelerikReporting
{
    [DataContract]
    public class Client
    {
        [DataMember(Name = "CLIENT_FIRSTNAME")]
        public string FirstName { get; set; }
        [DataMember(Name = "CLIENT_LASTNAME")]
        public string LastName { get; set; }
        [DataMember(Name = "CLIENT_FULLADDRESS")]
        public string FullAddress { get; set; }
        [DataMember(Name = "CLIENT_RESIDENCE")]
        public string Residence { get; set; }
        [DataMember(Name = "CLIENT_BIRTHDAY")]
        public string BirthDay { get; set; }
        [DataMember(Name = "CLIENT_BIRTHMONTH")]
        public string BirthMonth { get; set; }
        [DataMember(Name = "CLIENT_BIRTHYEAR")]
        public string BirthYear { get; set; }
        [DataMember(Name = "CLIENT_BIRTHPLACE")]
        public string BirthPlace { get; set; }
    }
}
