using System.Collections.Generic;
using Newtonsoft.Json;

namespace TagsCloudVisualization.WordsFileReading
{
    public class JsonParser : IParser
    {
        public IEnumerable<string> ParseText(string text)
        {
            var words = JsonConvert.DeserializeObject<string[]>(text);
            return words;
        }

        public string GetModeName()
        {
            return "json";
        }
    }
}
