using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace ECDiffieHellman
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var package = new Package
            {
                Age = DateTime.Now,
                Name = "Test Package",
                SaySomething = @"Lorem Ipsum is simply dummy text of the printing and typesetting industry. 
Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took 
a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but
also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s
with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing
software like Aldus PageMaker including versions of Lorem Ipsum."
            };

            var participant1 = new EcdhParty();
            var participant2 = new EcdhParty();

            participant1.SetOtherPartyPublicKey(participant2.PublicKey);
            participant2.SetOtherPartyPublicKey(participant1.PublicKey);
            var json = JsonConvert.SerializeObject(package);
            var (encryptedMessage, iv, hash) = participant1.Encrypt(json);
            Console.WriteLine("Encrypted object:");
            Console.WriteLine(Encoding.UTF32.GetString(encryptedMessage));
            //encryptedMessage[0] = Byte.MaxValue;
            var decryptedMessage = participant2.Decrypt(encryptedMessage, iv, hash);
            var decryptedPackage = JsonConvert.DeserializeObject<Package>(decryptedMessage);
            Console.WriteLine("Decrypted object:");
            Console.WriteLine(decryptedPackage);
        }
    }
}
