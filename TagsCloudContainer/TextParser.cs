using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TagsCloudContainer
{
    public class TextParser : ITextParser
    {
        private readonly IWordValidator wordValidator;

        public TextParser(IWordValidator wordValidator)
        {
            this.wordValidator = wordValidator;
        }

        public Dictionary<string, int> GetParsedText(string text)
        {
            var matches = Regex.Matches(text, @"\b\w+\b");
            var wordsEntry = matches
                .Select(x => x.Value.ToLower())
                .Where(x => wordValidator.IsValidWord(x))
                .GroupBy(x => x)
                .ToDictionary(x => x.Key, x => x.Count());

            return wordsEntry;
        }
    }
}