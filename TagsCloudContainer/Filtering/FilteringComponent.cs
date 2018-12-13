using System.Collections.Generic;

namespace TagsCloudContainer.Filtering
{
    public class FilteringComponent
    {
        private readonly List<IWordsFilter> filters;

        public FilteringComponent(List<IWordsFilter> filters)
        {
            this.filters = filters;
        }

        public FilteringComponent(FilteringSettings settings)
        {
            this.filters = settings.Filters;
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