using System.Collections.Generic;
using System.IO;

namespace TagsCloudVisualization.Parsers
{
    public class TxtParser : IParser
    {
        public IEnumerable<string> ParseWords(string filePath)
        {
            var reader = new StreamReader(filePath);
            
            var words = new List<string>();

            var line = "";

            while (line is not null)
            {
                line = reader.ReadLine();
                
                words.Add(line);
            }

            return words;
        }
    }
}