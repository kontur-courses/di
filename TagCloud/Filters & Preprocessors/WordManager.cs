using System.Collections.Generic;
using System.Linq;
using TagCloud.Interfaces;

namespace TagCloud
{
    public class WordManager 
    {
        private readonly IWordFilter[] filters;
        private readonly IWordProcessor[] processors;
        public WordManager(IWordFilter[] filters, IWordProcessor[] processors)
        {
            this.filters = filters;
            this.processors = processors;
        }
        public IEnumerable<string> ApplyFilters(IEnumerable<string> words)
        {
            var filteredWords = filters.Aggregate(words, 
                (currentWords, currentFilter) => currentFilter.Filter(currentWords));
            return processors.Aggregate(filteredWords, 
                (currentWords, currentFilter) => currentFilter.Preprocess(currentWords));
        }
    }
}  