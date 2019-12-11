using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TagCloud.TextProvider;

namespace TagCloud.TextParser
{
    public class TextParser : ITextParser
    {
        private readonly ITextProvider textProvider;
        private const string RegexPattern = @"\W|_";

        public TextParser(ITextProvider textProvider)
        {
            this.textProvider = textProvider;
        }

        public List<string> ParseText()
        {
            return ParseAllLines(textProvider.GetAllLines());
        }

        public List<string> ParseText(IEnumerable<string> paths)
        {
            return ParseAllLines(textProvider.GetAllLines(paths));
        }

        public List<string> ParseAllLines(IEnumerable<string> lines)
        {
            var allWords = new List<string>();
            foreach (var words in lines)
                allWords.AddRange(Regex.Split(words, RegexPattern, RegexOptions.IgnoreCase)
                    .Where(s => !string.IsNullOrEmpty(s)));
            return allWords;
        }
    }
}