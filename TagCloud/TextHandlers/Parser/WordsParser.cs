using System.Collections.Generic;
using System.IO;

namespace TagCloud.TextHandlers.Parser
{
    public class WordsParser : ITextParser
    {
        public IEnumerable<string> GetWords(string path)
        {
            return File.ReadAllLines(path);
        }
    }
}