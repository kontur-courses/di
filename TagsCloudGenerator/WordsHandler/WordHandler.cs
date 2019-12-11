using System.Collections.Generic;
using System.Linq;
using TagsCloudGenerator.WordsHandler.Converters;
using TagsCloudGenerator.WordsHandler.Filters;

namespace TagsCloudGenerator.WordsHandler
{
    public class WordHandler : IWordHandler
    {
        private readonly List<IWordsFilter> filters;
        private readonly List<IConverter> converters;

        public WordHandler(IEnumerable<IWordsFilter> filters, IEnumerable<IConverter> converters)
        {
            this.filters = filters.ToList();
            this.converters = converters.ToList();
        }

        public Dictionary<string, int> GetValidWords(Dictionary<string, int> wordToCount)
        {
            var validWords = ApplyConverters(wordToCount);
            validWords = ApplyFilters(validWords);

            return validWords;
        }

        private Dictionary<string, int> ApplyFilters(Dictionary<string, int> words)
        {
            return filters.Aggregate(words, (current, filter) => filter.Filter(current));
        }

        private Dictionary<string, int> ApplyConverters(Dictionary<string, int> words)
        {
            return converters.Aggregate(words, (current, converter) => converter.Convert(current));
        }
    }
}