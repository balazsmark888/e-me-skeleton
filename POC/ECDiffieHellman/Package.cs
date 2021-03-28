using System;

namespace ECDiffieHellman
{
    [Serializable]
    public class Package
    {
        public string Name { get; set; }

        public DateTime Age { get; set; }

        public string SaySomething { get; set; }

        public override string ToString()
        {
            return $"{Name}{Environment.NewLine}{Age:D}{Environment.NewLine}{SaySomething}";
        }
    }
}
