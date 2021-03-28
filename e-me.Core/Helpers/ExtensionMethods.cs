using System.Collections.Generic;
using Newtonsoft.Json;

namespace e_me.Core.Helpers
{
    public static class ExtensionMethods
    {
        public static Dictionary<string, string> ToFieldDictionary(this object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }
    }
}
