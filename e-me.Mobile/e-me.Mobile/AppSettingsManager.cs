using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace e_me.Mobile
{
    public class AppSettingsManager
    {
        private static AppSettingsManager _instance;
        private readonly JObject _secrets;

        private const string Namespace = "e_me.Mobile";
#if __IOS__
        private const string Namespace = "e_me.Mobile.iOS";
#endif
#if __ANDROID__
        private const string Namespace = "e_me.Mobile.Android";
#endif
        private const string FileName = "appsettings.json";

        private AppSettingsManager()
        {
            try
            {
                var assembly = typeof(AppSettingsManager).GetTypeInfo().Assembly;
                var stream = assembly.GetManifestResourceStream($"{Namespace}.{FileName}");
                using var reader = new StreamReader(stream);
                var json = reader.ReadToEnd();
                _secrets = JObject.Parse(json);
            }
            catch (Exception)
            {
                Debug.WriteLine("Unable to load secrets file");
            }
        }

        public static AppSettingsManager Settings => _instance ??= new AppSettingsManager();

        public string this[string name]
        {
            get
            {
                try
                {
                    var path = name.Split(':');

                    var node = _secrets[path[0]];
                    for (var index = 1; index < path.Length; index++)
                    {
                        node = node[path[index]];
                    }

                    return node.ToString();
                }
                catch (Exception)
                {
                    Debug.WriteLine($"Unable to retrieve secret '{name}'");
                    return string.Empty;
                }
            }
        }
    }
}
