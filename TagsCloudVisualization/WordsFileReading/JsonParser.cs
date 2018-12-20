using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace TagsCloudVisualization.WordsFileReading
{
    public class JsonParser : IParser
    {
        public IEnumerable<string> ParseText(TextReader textReader)
        {
            var serializer = new JsonSerializer();

            using (textReader)
            using (var jsonTextReader = new JsonTextReader(textReader))
            {
                return serializer.Deserialize<string[]>(jsonTextReader);
            }
        }

        public string GetModeName()
        {
            return "json";
        }
    }
}
