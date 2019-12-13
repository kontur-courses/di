using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.Core.TextHandler.WordFilters
{
    class Filter
    {
        private readonly IWordFilter[] wordFilter;
        public HashSet<string> UserExcludedWords { get; set; }

        public Filter(IWordFilter[] wordFilter)
        {
            this.wordFilter = wordFilter;
            UserExcludedWords = new HashSet<string>();
        }

        public IEnumerable<string> FilterWords(IEnumerable<string> words) => words.Where(FilterWord);

        public bool FilterWord(string word) => wordFilter.All(filter => filter.HaveToTake(word) && !UserExcludedWords.Contains(word));
    }
}