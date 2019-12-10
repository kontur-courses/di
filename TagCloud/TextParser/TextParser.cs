using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TagCloud.TextProvider
{
    public class TextParser : ITextParser
    {
        private readonly ITextProvider textProvider;
        private const string RegexPattern = @"\W|_";

        public TextParser(ITextProvider textProvider)
        {
            this.textProvider = textProvider;
        }

        public List<string> ParseText() => ParseAllLines(textProvider.GetAllLines());

        public List<string> ParseText(IEnumerable<string> paths) => ParseAllLines(textProvider.GetAllLines(paths));

        public List<string> ParseAllLines(IEnumerable<string> lines)
        {
            var allWords = new List<string>();
            foreach (var words in lines)
                allWords.AddRange(Regex.Split(words, RegexPattern, RegexOptions.IgnoreCase)
                    .Select(s => s.MakeLettersLowerCase()).Where(s=>!string.IsNullOrEmpty(s)));
            return allWords;
        }
    }
}