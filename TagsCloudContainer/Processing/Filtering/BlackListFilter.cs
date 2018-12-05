using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.Processing.Filtering
{
    public class BlackListFilter : IWordFilter
    {
        private readonly HashSet<string> wordsToFilter;

        public BlackListFilter(IEnumerable<string> wordsToFilter)
        {
            this.wordsToFilter = new HashSet<string>(wordsToFilter);
        }

        public IEnumerable<string> Filter(IEnumerable<string> words) =>
            words.Where(word => !wordsToFilter.Contains(word));
    }
}