using System.IO;
using ResultProject;

namespace TagsCloudVisualization.Readers
{
    internal class TxtReader : IFileReader
    {
        public TextFormat Format => TextFormat.Txt;

        public Result<string> ReadFile(string filePath)
        {
            return Result.Of(() => File.ReadAllText(filePath), $"Can't read {filePath} for some reason");
        }
    }
}