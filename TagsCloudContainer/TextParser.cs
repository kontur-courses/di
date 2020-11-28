using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TagsCloudContainer
{
    public class TextParser : ITextParser
    {
        private readonly HashSet<string> stopWords;

        public TextParser(HashSet<string> stopWords)
        {
            this.stopWords = stopWords;
        }

        public Dictionary<string, int> GetParsedText(string text)
        {
            var matches = Regex.Matches(text, @"\b\w+\b");
            var words = matches
                .Select(x => x.Value.ToLower())
                .Where(x => !stopWords.Contains(x))
                .GroupBy(x => x)
                .ToDictionary(x => x.Key, x => x.Count());

            return words;
        }
    }
}