using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TagsCloudGenerator.Tools
{
    public class WordsParser: IWordsParser
    {
        public List<string> Parse(string text)
        {
            var matches = Regex.Matches(text, @"\b[\w']*\b");

            return matches
                .Cast<Match>()
                .Where(m => !string.IsNullOrEmpty(m.Value))
                .Select(m => TrimSuffix(m.Value)).ToList();
        }

        private static string TrimSuffix(string word)
        {
            var apostropheLocation = word.IndexOf('\'');

            if (apostropheLocation == -1)
                return word;

            var length = IsAbbreviationOfWordNot(word)
                ? apostropheLocation - 1
                : apostropheLocation;

            return word.Substring(0, length);
        }

        private static bool IsAbbreviationOfWordNot(string word)
        {
            return Regex.IsMatch(word, @"[a-zA-Z]n't");
        }
    }
}
