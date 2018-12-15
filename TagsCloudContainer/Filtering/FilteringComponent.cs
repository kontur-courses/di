using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.Filtering
{
    public class FilteringComponent
    {
        private readonly List<IWordsFilter> filters;

        public FilteringComponent(IWordsFilter[] filters)
        {
            this.filters = filters.ToList();
        }

        public List<string> FilterWords(List<string> words)
        {
            foreach (var filter in filters)
            {
                words = filter.Filter(words);
            }

            return words;
        }

        public void AddFilter(IWordsFilter wordsFilter)
        {
            filters.Add(wordsFilter);
        }
    }
}