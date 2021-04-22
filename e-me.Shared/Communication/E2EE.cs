using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace e_me.Shared.Communication
{
    public static class E2EE
    {
        public static (byte[] EncryptedMessage, byte[] Hash) Encrypt(string message, byte[] sharedKey, byte[] iv, byte[] hmacKey, byte[] derivedHmacKey)
        {
            using var aes = new AesCryptoServiceProvider
            {
                Key = sharedKey,
                IV = iv,
            };
            using var memoryStream = new MemoryStream();
            using var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
            var plaintextMessage = Encoding.UTF8.GetBytes(message);
            cryptoStream.Write(plaintextMessage, 0, plaintextMessage.Length);
            cryptoStream.Close();
            var encryptedMessage = memoryStream.ToArray();
            return (encryptedMessage, GetDoubleHash(encryptedMessage, hmacKey, derivedHmacKey));
        }

        public static string Decrypt(byte[] encryptedMessage, byte[] iv, byte[] hash, byte[] sharedKey, byte[] hmacKey, byte[] derivedHmacKey)
        {
            if (!IsValidHash(encryptedMessage, hash, hmacKey, derivedHmacKey)) throw new CryptographicException("Hash is invalid!");
            using var aes = new AesCryptoServiceProvider
            {
                Key = sharedKey,
                IV = iv
            };
            using var memoryStream = new MemoryStream();
            using var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(encryptedMessage, 0, encryptedMessage.Length);
            cryptoStream.Close();
            var message = Encoding.UTF8.GetString(memoryStream.ToArray());
            return message;
        }

        public static bool IsValidHash(byte[] message, byte[] hash, byte[] hmacKey, byte[] derivedHmacKey)
        {
            var hashedMessage = GetDoubleHash(message, hmacKey, derivedHmacKey);
            return hashedMessage.SequenceEqual(hash);
        }

        private static byte[] GetDoubleHash(byte[] message, byte[] hmacKey, byte[] derivedHmacKey)
        {
            using var hmac1 = new HMACSHA256(hmacKey);
            var hash1 = hmac1.ComputeHash(message);
            using var hmac2 = new HMACSHA256(derivedHmacKey);
            var hash2 = hmac2.ComputeHash(hash1);
            return hash2;
        }
    }
}
