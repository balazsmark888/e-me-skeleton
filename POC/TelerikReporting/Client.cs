using System.Runtime.Serialization;

namespace TelerikReporting
{
    [DataContract]
    public class Client
    {
        [DataMember(Name = "###CLIENT.FIRSTNAME###")]
        public string FirstName { get; set; }
        [DataMember(Name = "###CLIENT.LASTNAME###")]
        public string LastName { get; set; }
        [DataMember(Name = "###CLIENT.FULLADDRESS###")]
        public string FullAddress { get; set; }
        [DataMember(Name = "###CLIENT.RESIDENCE###")]
        public string Residence { get; set; }
        [DataMember(Name = "###CLIENT.BIRTHDAY###")]
        public string BirthDay { get; set; }
        [DataMember(Name = "###CLIENT.BIRTHMONTH###")]
        public string BirthMonth { get; set; }
        [DataMember(Name = "###CLIENT.BIRTHYEAR###")]
        public string BirthYear { get; set; }
        [DataMember(Name = "###CLIENT.BIRTHPLACE###")]
        public string BirthPlace { get; set; }
    }
}
