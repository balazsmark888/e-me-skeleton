namespace e_me.Mobile.Models
{
    public class EcdhKeyInformation
    {
        public byte[] SharedKey { get; set; }

        public byte[] IV { get; set; }

        public byte[] HmacKey { get; set; }

        public byte[] DerivedHmacKey { get; set; }

        public byte[] ServerPublicKey { get; set; }

        public byte[] ClientPublicKey { get; set; }
    }
}
