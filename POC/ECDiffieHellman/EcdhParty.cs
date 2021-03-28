using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ECDiffieHellman
{
    public class EcdhParty : IDisposable
    {
        public ECDiffieHellmanPublicKey PublicKey { get; set; }

        private ECDiffieHellmanPublicKey _otherPartyPublicKey;

        private byte[] _aesKey;

        private byte[] _hmacKey;

        private byte[] _derivedHmacKey;

        private readonly ECDiffieHellmanCng _diffieHellmanCng;

        public EcdhParty()
        {
            _diffieHellmanCng = new ECDiffieHellmanCng
            {
                KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hmac,
                HashAlgorithm = CngAlgorithm.Sha256,
            };
            PublicKey = _diffieHellmanCng.PublicKey;
        }

        public void SetOtherPartyPublicKey(ECDiffieHellmanPublicKey publicKey)
        {
            _otherPartyPublicKey = publicKey;
            _aesKey = _diffieHellmanCng.DeriveKeyMaterial(publicKey);
            _hmacKey = _aesKey;
            _diffieHellmanCng.HmacKey = _aesKey;
            _derivedHmacKey = _diffieHellmanCng.DeriveKeyFromHmac(_otherPartyPublicKey, HashAlgorithmName.SHA256, _hmacKey);
        }

        public (byte[] message, byte[] iv, byte[] hash) Encrypt(string message)
        {
            using var aes = new AesCryptoServiceProvider
            {
                Key = _aesKey,
            };
            using var memoryStream = new MemoryStream();
            using var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
            var plaintextMessage = Encoding.UTF8.GetBytes(message);
            cryptoStream.Write(plaintextMessage, 0, plaintextMessage.Length);
            cryptoStream.Close();
            var encryptedMessage = memoryStream.ToArray();
            var iv = aes.IV;
            using var hmac1 = new HMACSHA256(_hmacKey);
            var hash1 = hmac1.ComputeHash(encryptedMessage);
            using var hmac2 = new HMACSHA256(_derivedHmacKey);
            var hash2 = hmac2.ComputeHash(hash1);
            return (encryptedMessage, iv, hash2);
        }

        public string Decrypt(byte[] encryptedMessage, byte[] iv, byte[] hash)
        {
            if (!IsValidHash(encryptedMessage, hash)) throw new CryptographicException("Hash is invalid!");
            using var aes = new AesCryptoServiceProvider
            {
                Key = _aesKey,
                IV = iv
            };
            using var memoryStream = new MemoryStream();
            using var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(encryptedMessage, 0, encryptedMessage.Length);
            cryptoStream.Close();
            var message = Encoding.UTF8.GetString(memoryStream.ToArray());
            return message;
        }

        public bool IsValidHash(byte[] message, byte[] hash)
        {
            using var hmac1 = new HMACSHA256(_hmacKey);
            var hash1 = hmac1.ComputeHash(message);
            using var hmac2 = new HMACSHA256(_derivedHmacKey);
            var hash2 = hmac2.ComputeHash(hash1);
            return hash2.SequenceEqual(hash);
        }

        public void Dispose()
        {
            _diffieHellmanCng.Dispose();
        }
    }
}
