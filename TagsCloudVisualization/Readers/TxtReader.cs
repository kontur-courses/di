using System.IO;

namespace TagsCloudVisualization.Readers
{
    public class TxtReader : IFileReader
    {
        public TextFormat Format => TextFormat.Txt;

        public string ReadFile(string filePath)
        {
            return File.ReadAllText(filePath);
        }
    }
}