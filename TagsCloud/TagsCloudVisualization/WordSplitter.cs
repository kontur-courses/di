using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TagsCloudVisualization
{
    public static class WordSplitter
    {
        public static IEnumerable<string> Split(string input)
        {
            var matches = Regex.Matches(input, @"\b[\w']*\b");
            return matches.Where(m => !string.IsNullOrEmpty(m.Value)).Select(m => TrimSuffix(m.Value));
        }

        private static string TrimSuffix(string word)
        {
            var apostropheLocation = word.IndexOf('\'');
            if (apostropheLocation != -1) word = word[..apostropheLocation];
            return word;
        }
    }
}