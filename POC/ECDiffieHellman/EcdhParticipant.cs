using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ECDiffieHellman
{
    public class EcdhParticipant : IDisposable
    {
        public byte[] PublicKey { get; set; }

        private byte[] _aesKey;

        private readonly ECDiffieHellmanCng _diffieHellmanCng;

        public EcdhParticipant()
        {
            _diffieHellmanCng = new ECDiffieHellmanCng
            {
                KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hmac,
                HashAlgorithm = CngAlgorithm.Sha256
            };
            PublicKey = _diffieHellmanCng.PublicKey.ToByteArray();
        }

        public void SetClientPublicKey(byte[] publicKey)
        {
            var cngKey = CngKey.Import(publicKey, CngKeyBlobFormat.EccPublicBlob);
            _aesKey = _diffieHellmanCng.DeriveKeyMaterial(cngKey);
        }

        public (byte[] message, byte[] iv) Encrypt(string secretMessage)
        {
            using var aes = new AesCryptoServiceProvider { Key = _aesKey };
            var iv = aes.IV;

            using var cipherText = new MemoryStream();
            using var cs = new CryptoStream(cipherText, aes.CreateEncryptor(), CryptoStreamMode.Write);
            var plaintextMessage = Encoding.UTF8.GetBytes(secretMessage);
            cs.Write(plaintextMessage, 0, plaintextMessage.Length);
            cs.Close();
            var encryptedMessage = cipherText.ToArray();
            return (encryptedMessage, iv);
        }

        public string Decrypt(byte[] encryptedMessage, byte[] iv)
        {
            using var aes = new AesCryptoServiceProvider { Key = _aesKey, IV = iv };
            using var plaintext = new MemoryStream();
            using var cs = new CryptoStream(plaintext, aes.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(encryptedMessage, 0, encryptedMessage.Length);
            cs.Close();
            var message = Encoding.UTF8.GetString(plaintext.ToArray());
            return message;
        }

        public void Dispose()
        {
            _diffieHellmanCng.Dispose();
        }
    }
}
