using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TagsCloudApp.WordsLoading
{
    public class WordsParser : IWordsParser
    {
        private static readonly Regex wordRegex = new(@"[\p{L}-]+");

        public IEnumerable<string> Parse(string text)
        {
            foreach (Match match in wordRegex.Matches(text))
                yield return match.Value;
        }
    }
}