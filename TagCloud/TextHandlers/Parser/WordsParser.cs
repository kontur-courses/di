using System.Collections.Generic;
using System.IO;
using TagsCloudVisualization;

namespace TagCloud.TextHandlers.Parser
{
    public class WordsParser : ITextParser
    {

        public WordsParser(string path)
        {
        }

        public IEnumerable<string> GetWords(string path)
        {
            return File.ReadAllLines(path);
        }
    }
}