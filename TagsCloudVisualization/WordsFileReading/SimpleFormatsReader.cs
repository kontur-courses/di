using System.IO;
using System.Text;

namespace TagsCloudVisualization.WordsFileReading
{
    public class SimpleFormatsReader : IFileReader
    { 
        public TextReader ReadText(string fileName)
        {
            return new StreamReader(fileName, Encoding.UTF8);
        }

        public string[] SupportedTypes()
        {
            return new[] {"txt", "json"};
        }
    }
}
