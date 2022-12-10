using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TagCloud.TextParsing
{
    public class TextParser : ITextParser
    {
        private readonly Regex regex = new Regex(@"\w+", RegexOptions.Compiled);

        public IReadOnlyList<string> GetWords(string text)
        {
            var words = new List<string>();

            var matches = regex.Matches(text);

            foreach (Match match in matches)
                words.Add(match.Value);

            return words;
        }
    }
}
    