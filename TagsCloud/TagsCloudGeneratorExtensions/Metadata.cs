using System.IO;

namespace TagsCloudGeneratorExtensions
{
    public static class Metadata
    {
        public static string PathToMyStem { get; set; }

        static Metadata() => PathToMyStem =
            string.Join(
                Path.DirectorySeparatorChar.ToString(),
                "..", "..", "..", "mystem.exe");
    }
}