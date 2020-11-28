using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TagCloud.Core.WordConverters;
using TagCloud.Core.WordsFilters;

namespace TagCloud.Core.WordsProcessors
{
    public class WordsProcessor : IWordsProcessor
    {
        private readonly List<IWordFilter> filters;
        private readonly List<IWordConverter> converters;

        public WordsProcessor(IEnumerable<IWordFilter> filters,
            IEnumerable<IWordConverter> converters)
        {
            this.converters = converters.ToList();
            this.filters = filters.ToList();
        }

        public IEnumerable<string> Process(IEnumerable<string> words)
        {
            return words.Where(IsValid).Select(Convert);
        }

        private string Convert(string word)
        {
            return converters.Aggregate(word,
                (current, wordHandler) => wordHandler.Convert(current));
        }

        private bool IsValid(string word)
        {
            return filters.All(filter => filter.IsValid(word));
        }
    }
}