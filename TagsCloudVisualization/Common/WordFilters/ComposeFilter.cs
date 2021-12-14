using System.Collections.Generic;

namespace TagsCloudVisualization.Common.WordFilters
{
    public class ComposeFilter : IWordFilter
    {
        private readonly IWordFilter[] filters;

        public ComposeFilter(IWordFilter[] filters = null)
        {
            this.filters = filters ?? new IWordFilter[0];
        }

        public IEnumerable<string> Filter(IEnumerable<string> words)
        {
            foreach (var filter in filters)
                words = filter.Filter(words);

            return words;
        }
    }
}