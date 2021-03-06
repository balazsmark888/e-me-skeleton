using System;
using System.Security.Cryptography;

namespace e_me.Core.Communication
{
    public class ECDiffieHellmanExchangeHandler : IDisposable
    {
        private byte[] _privateKey;

        public byte[] PublicKey { get; private set; }

        public ECDiffieHellmanCng EcDiffieHellmanCng { get; private set; }

        public ECDiffieHellmanExchangeHandler()
        {
            Init();
        }

        private void Init()
        {
            EcDiffieHellmanCng = new ECDiffieHellmanCng
            {
                KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash,
                HashAlgorithm = CngAlgorithm.Sha512
            };
            PublicKey = EcDiffieHellmanCng.PublicKey.ToByteArray();
        }

        public void CreatePrivateKey(byte[] partnerPublicKey)
        {
            _privateKey = EcDiffieHellmanCng.DeriveKeyMaterial(CngKey.Import(partnerPublicKey, CngKeyBlobFormat.EccPublicBlob));
        }

        public void Dispose()
        {
            EcDiffieHellmanCng?.Dispose();
        }
    }
}
