using System;
using System.Collections.Generic;

namespace e_me.Core.Helpers
{
    public static class PasswordGeneration
    {
        private static readonly char[] UpperCaseLetters = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        private static readonly char[] LowerCaseLetters = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        private static readonly char[] Digits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        public static string GenerateRandomPassword()
        {
            return GenerateNCharPassword(16);
        }

        public static string GenerateNCharPassword(int numberOfChar)
        {
            var password = string.Empty;
            var sets = new List<char[]> { UpperCaseLetters, LowerCaseLetters, Digits };
            var random = new Random();
            for (var i = 0; i < numberOfChar; i++)
            {
                var setIndex = random.Next(sets.Count);
                var set = sets[setIndex];
                var charIndex = random.Next(set.Length);
                var character = set[charIndex];
                password += character;
            }

            return password;
        }

        public static string GenerateNDigitsNumber(int numberOfChar)
        {
            var digits = string.Empty;
            var sets = new List<char[]> { Digits };
            var random = new Random();
            for (var i = 0; i < numberOfChar; i++)
            {
                var setIndex = random.Next(sets.Count);
                var set = sets[setIndex];
                var charIndex = random.Next(set.Length);
                var character = set[charIndex];
                digits += character;
            }

            return digits;
        }
    }
}
