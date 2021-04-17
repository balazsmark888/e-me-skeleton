using System.Security.Cryptography;

namespace e_me.Shared.Communication
{
    public class EcdhKeyStore
    {
        public byte[] PrivateKey { get; }

        public byte[] PublicKey { get; }

        public byte[] PeerPublicKey { get; private set; }

        public byte[] SharedKey { get; private set; }

        public byte[] IV { get; set; }

        public byte[] HmacKey { get; private set; }

        public byte[] DerivedHmacKey { get; private set; }

        private const int Iterations = 1000;
        private const int KeySize = 32;
        private const int SaltSize = 8;
        private const int RawKeySize = 140;

        public EcdhKeyStore()
        {
            using var rngCsp = new RNGCryptoServiceProvider();
            var salt = new byte[SaltSize];
            var rawKey = new byte[RawKeySize];
            rngCsp.GetBytes(salt);
            rngCsp.GetBytes(rawKey);
            PrivateKey = new Rfc2898DeriveBytes(rawKey, salt, Iterations, HashAlgorithmName.SHA512).GetBytes(KeySize);
            PublicKey = Curve25519.GetPublicKey(PrivateKey);
        }

        public EcdhKeyStore(byte[] peerPublicKey) : this()
        {
            SetOtherPartyPublicKey(peerPublicKey);
        }

        public void SetOtherPartyPublicKey(byte[] peerPublicKey)
        {
            PeerPublicKey = peerPublicKey;
            SharedKey = Curve25519.GetSharedSecret(PrivateKey, peerPublicKey);
            HmacKey = SharedKey;
            DerivedHmacKey = Curve25519.GetSharedSecret(HmacKey, HmacKey);
            using var aesProvider = new AesCryptoServiceProvider
            {
                Key = SharedKey
            };
            aesProvider.GenerateIV();
            IV = aesProvider.IV;
        }
    }
}
