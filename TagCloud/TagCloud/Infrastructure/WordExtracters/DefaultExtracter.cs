using System;
using System.Collections.Generic;
using System.Linq;

namespace TagCloud
{
    public class DefaultExtracter : IExtracter
    {
        private readonly Dictionary<string, int> frequencyDictionary;
        private readonly IParser parser;

        public DefaultExtracter(IParser parser)
        {
            this.parser = parser;
            frequencyDictionary = new Dictionary<string, int>();
        }

        public List<WordToken> ExtractWordTokens(string text)
        {
            if (text == null)
                throw new ArgumentNullException();

            var words = text.Split('\r', '\n');
            var boringWords = ExtracterHelper.GetBoringWords();
            var parsedWords = words.Select(word => parser.Parse(word));
            var interestingWords = parsedWords.Where(word => !boringWords.Contains(word) && word != "");
            var frequencyDictionary = interestingWords
                .GroupBy(word => word)
                .ToDictionary(group => group.Key, group => group.Count());
            var wordTokens = frequencyDictionary
                .Select(pair => new WordToken(pair.Key, pair.Value))
                .ToList();
            return wordTokens;
        }
    }
}
