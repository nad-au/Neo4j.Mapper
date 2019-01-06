using System.IO;
using System.Reflection;

namespace Queries
{
    public static class Utils
    {
        public static string GetResourceContent(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
