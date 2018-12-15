using System.IO;
using System.Text;

namespace TagsCloudVisualization.WordsFileReading
{
    public class SimpleFormatsReader : IFileReader
    { 
        public string ReadText(string fileName)
        {
            using (var reader = new StreamReader(fileName, Encoding.UTF8))
                return reader.ReadToEnd();
        }

        public string[] SupportedTypes()
        {
            return new[] {"txt", "json"};
        }
    }
}
