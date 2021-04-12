using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace e_me.Shared
{
    public static class Helpers
    {
        public static Dictionary<string, string> MapToDictionary(this object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            return dict;
        }

        public static string ToBase64String(this byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        public static byte[] FromBase64String(this string text)
        {
            return Convert.FromBase64String(text);
        }
    }
}
