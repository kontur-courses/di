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

            var words = from m in matches.Cast<Match>()
                where !string.IsNullOrEmpty(m.Value)
                select TrimSuffix(m.Value);

            return words.ToList();
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
