using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer.App.DictionaryGenerator
{
    internal class DictionaryGenerator : IDictionaryGenerator
    {
        private readonly ITextParser textParser;
        private readonly IWordFilter wordFilter;
        private readonly IWordNormalizer wordProcessor;

        private DictionaryGenerator(ITextParser textParser,
            IWordNormalizer wordProcessor, IWordFilter wordFilter)
        {
            this.textParser = textParser;
            this.wordProcessor = wordProcessor;
            this.wordFilter = wordFilter;
        }

        public Dictionary<string, int> GenerateDictionary(IEnumerable<string> lines)
        {
            var words = textParser.GetWords(lines);
            var notBoringWords = wordFilter.FilterOutBoringWords(
                words.Select(word => wordProcessor.NormalizeWord(word)));
            var dictionary = new Dictionary<string, int>();
            foreach (var word in notBoringWords)
            {
                if (!dictionary.ContainsKey(word)) dictionary[word] = 0;

                dictionary[word]++;
            }

            return dictionary;
        }
    }
}