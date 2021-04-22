namespace e_me.Mobile.Models
{
    public class EcdhKeyInformation
    {
        public string SharedKey { get; set; }
        public string IV { get; set; }
        public string HmacKey { get; set; }
        public string DerivedHmacKey { get; set; }
        public string ServerPublicKey { get; set; }
        public string ClientPublicKey { get; set; }
    }
}
