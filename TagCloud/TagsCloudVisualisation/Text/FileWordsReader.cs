using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace TagsCloudVisualisation.Text
{
    public class FileWordsReader : IFileWordsReader
    {
        private static readonly Regex wordsRegex = new Regex(
            @"(\w+)\W?",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public string[] GetWordsFrom(string path)
        {
            var text = File.ReadAllText(path);
            var allWords = new List<string>();
            var match = wordsRegex.Match(text);
            while (match.Success)
            {
                allWords.Add(match.Captures[0].Value);
                match = match.NextMatch();
            }

            return allWords.ToArray();
        }
    }
}