using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace TagsCloudVisualization.WordsFileReading
{
    public class JsonFileReader : IFileReader
    {
        public IEnumerable<string> ReadAllWords(string fileName)
        {
            using (var reader = new StreamReader(fileName, Encoding.UTF8))
            {
                var content = reader.ReadToEnd();
                var words = JsonConvert.DeserializeObject<string[]>(content);
                return words;
            }
        }

        public string[] SupportedTypes()
        {
            return new[] {"json"};
        }
    }
}
