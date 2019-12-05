using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.TextFilters;
using TagsCloudVisualization.WordConverters;

namespace TagsCloudVisualization.TextPreprocessing
{
    public class WordPreprocessor
    {
        private readonly IEnumerable<ITextFilter> filters;
        private readonly IEnumerable<IWordConverter> wordConverters;

        public WordPreprocessor(IEnumerable<ITextFilter> filters, IEnumerable<IWordConverter> wordConverters)
        {
            this.filters = filters;
            this.wordConverters = wordConverters;
        }

        public IEnumerable<string> GetPreprocessedWords(IEnumerable<string> words)
        {
            var convertedWords =
                wordConverters.Aggregate(words, (current, wordConverter) => wordConverter.ConvertWords(current));
            return filters.Aggregate(convertedWords, (current, textFilter) => textFilter.FilterWords(current));
        }
    }
}