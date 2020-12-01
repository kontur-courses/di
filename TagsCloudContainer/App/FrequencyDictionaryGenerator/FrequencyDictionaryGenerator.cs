using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Infrastructure.DictionaryGenerator;

namespace TagsCloudContainer.App.FrequencyDictionaryGenerator
{
    internal class FrequencyDictionaryGenerator : IFrequencyDictionaryGenerator
    {
        private readonly ITextParser textParser;
        private readonly IEnumerable<IWordFilter> wordFilters;
        private readonly IEnumerable<IWordNormalizer> wordNormalizers;

        public FrequencyDictionaryGenerator(ITextParser textParser,
            IEnumerable<IWordNormalizer> wordNormalizers, IEnumerable<IWordFilter> wordFilters)
        {
            this.textParser = textParser;
            this.wordNormalizers = wordNormalizers;
            this.wordFilters = wordFilters;
        }

        public Dictionary<string, double> GenerateFrequencyDictionary(IEnumerable<string> lines)
        {
            var notBoringWords = textParser.GetWords(lines)
                .NormalizeWords(wordNormalizers)
                .FilterOutBoringWords(wordFilters);
            var notBoringWordsCount = notBoringWords.Count();
            return notBoringWords
                .GroupBy(word => word)
                .ToDictionary(group => group.Key,
                    group => (double) group.Count() / notBoringWordsCount);
        }
    }
}