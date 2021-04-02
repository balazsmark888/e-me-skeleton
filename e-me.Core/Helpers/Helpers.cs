using System.Collections.Generic;
using Newtonsoft.Json;

namespace e_me.Core.Helpers
{
    public static class Helpers
    {
        public static Dictionary<string, string> MapToDictionary(this object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            return dict;
        }
    }
}
