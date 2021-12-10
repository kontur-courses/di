using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace TagCloud.TextHandlers.Parser
{
    public class WordsFromTextParser : ITextParser
    {
        public IEnumerable<string> GetWords(string path)
        {
            var input = File.ReadAllText(path);
            var matches = Regex.Matches(input, @"\b[\w']*\b");

            var words = matches.Cast<Match>()
                .Where(m => !string.IsNullOrEmpty(m.Value))
                .Select(m => m.Value);

            return words;
        }
    }
}