using UCMS.Cryptography;

namespace e_me.Core.Helpers
{
    public static class Cryptography
    {
        public static string Encrypt(string stringToEncrypt)
        {
            var encryptor = new Encryptor();
            return encryptor.Encrypt(stringToEncrypt);
        }

        public static string Decrypt(string stringToDecrypt)
        {
            var encryptor = new Encryptor();
            return encryptor.Decrypt(stringToDecrypt);
        }

        public static string HashMD5(string stringToHash)
        {
            var encryptor = new Encryptor();
            return encryptor.HashMD5(stringToHash);
        }

        public static string HashSha512(string stringToHash)
        {
            var encryptor = new Encryptor();
            return encryptor.HashSha512(stringToHash);
        }

        public static string GetEncryptedPassword(string password)
        {
            return Encrypt(password);
        }
    }
}
