using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.Core.TextHandler.WordFilters
{
    class Filter
    {
        private readonly IWordFilter[] wordFilter;

        public Filter(IWordFilter[] wordFilter)
        {
            this.wordFilter = wordFilter;
        }

        public IEnumerable<string> FilterWords(IEnumerable<string> words) => words.Where(FilterWord);

        public bool FilterWord(string word) => wordFilter.All(filter => filter.HaveToTake(word));
    }
}