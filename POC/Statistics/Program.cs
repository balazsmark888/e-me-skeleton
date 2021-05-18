using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using ECDiffieHellman;
using Newtonsoft.Json;

namespace Statistics
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var text = new string(Enumerable.Repeat(chars, 1024 * 5).Select(s => s[random.Next(s.Length)]).ToArray());
            var encryptionTimes = new List<long>();
            var decryptionTimes = new List<long>();
            var messageSizes = new List<int>();
            var n = 300;
            for (var i = 1; i <= n; i++)
            {
                text += new string(Enumerable.Repeat(chars, 1024 * 5).Select(s => s[random.Next(s.Length)]).ToArray());
                var bytes = Encoding.ASCII.GetBytes(text);
                var participant1 = new EcdhParty();
                var participant2 = new EcdhParty();

                participant1.SetOtherPartyPublicKey(participant2.PublicKey);
                participant2.SetOtherPartyPublicKey(participant1.PublicKey);
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                var (encryptedMessage, iv, hash) = participant1.Encrypt(text);
                stopWatch.Stop();
                messageSizes.Add(bytes.Length);
                encryptionTimes.Add(stopWatch.ElapsedTicks);
                stopWatch.Reset();
                stopWatch.Start();
                var decryptedMessage = participant2.Decrypt(encryptedMessage, iv, hash);
                stopWatch.Stop();
                decryptionTimes.Add(stopWatch.ElapsedTicks);
            }
            Console.WriteLine(string.Join(",", messageSizes));
            Console.WriteLine();
            Console.WriteLine(string.Join(",", encryptionTimes));
            Console.WriteLine();
            Console.WriteLine(string.Join(",", decryptionTimes));
        }
    }
}

