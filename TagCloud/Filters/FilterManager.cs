using System.Collections.Generic;
using System.Linq;
using TagCloud.Interfaces;
namespace TagCloud.Filters
{
    public class FilterManager 
    {
        private readonly IWordFilter[] filters;
        public FilterManager(IWordFilter[] filters)
        {
            this.filters = filters;
        }
        public IEnumerable<string> ApplyFilters(IEnumerable<string> words)
        {
            return filters.Aggregate(words, 
                (currentWords, currentFilter) => currentFilter.Filter(currentWords));
        }
    }
}  