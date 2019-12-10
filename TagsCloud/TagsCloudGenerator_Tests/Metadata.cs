using System.IO;
using System.Reflection;

namespace TagsCloudGenerator_Tests
{
    internal static class Metadata
    {
        public static readonly string WorkingDirectory =
            string.Join(
                Path.DirectorySeparatorChar.ToString(),
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "TestData") + Path.DirectorySeparatorChar;
    }
}