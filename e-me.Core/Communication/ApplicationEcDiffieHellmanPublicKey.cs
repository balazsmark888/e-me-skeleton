using System.Security.Cryptography;

namespace e_me.Core.Communication
{
    public class ApplicationEcDiffieHellmanPublicKey : ECDiffieHellmanPublicKey
    {
        public ApplicationEcDiffieHellmanPublicKey(byte[] publicKey) : base(publicKey)
        {
        }
    }
}
