using System.IO;
using System.Reflection;

namespace TagsCloudGeneratorExtensions
{
    internal static class Metadata
    {
        public static string PathToMyStem =>
                string.Join(
                    Path.DirectorySeparatorChar.ToString(),
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    "Resources",
                    "mystem.exe");
    }
}