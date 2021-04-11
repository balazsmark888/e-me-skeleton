using System.IO;
using System.Reflection;

namespace e_me.Shared
{
    public class Utils
    {
        public static void ExtractSaveResource(string filename, string location)
        {
            var a = Assembly.GetExecutingAssembly();
            using var resourceStream = a.GetManifestResourceStream(filename);
            if (resourceStream == null) return;
            var full = Path.Combine(location, filename);
            using var stream = File.Create(full);
            resourceStream.CopyTo(stream);
        }
    }
}
