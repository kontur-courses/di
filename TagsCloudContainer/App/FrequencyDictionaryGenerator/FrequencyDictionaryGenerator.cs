using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Infrastructure.DictionaryGenerator;

namespace TagsCloudContainer.App.FrequencyDictionaryGenerator
{
    internal class FrequencyDictionaryGenerator : IFrequencyDictionaryGenerator
    {
        private readonly ITextParser textParser;
        private readonly IWordFilter wordFilter;
        private readonly IWordNormalizer wordProcessor;

        public FrequencyDictionaryGenerator(ITextParser textParser,
            IWordNormalizer wordProcessor, IWordFilter wordFilter)
        {
            this.textParser = textParser;
            this.wordProcessor = wordProcessor;
            this.wordFilter = wordFilter;
        }

        public Dictionary<string, double> GenerateFrequencyDictionary(IEnumerable<string> lines)
        {
            var words = textParser.GetWords(lines);
            var notBoringWords = wordFilter.FilterOutBoringWords(
                words.Select(word => wordProcessor.NormalizeWord(word))).ToArray();
            var dictionary = new Dictionary<string, double>();
            foreach (var word in notBoringWords)
            {
                if (!dictionary.ContainsKey(word)) dictionary[word] = 0;
                dictionary[word] += 1;
            }

            foreach (var key in dictionary.Keys.ToArray()) dictionary[key] /= notBoringWords.Length;

            return dictionary;
        }
    }
}