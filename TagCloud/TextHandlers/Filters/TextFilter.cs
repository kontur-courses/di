using System;
using System.Collections.Generic;
using System.Linq;

namespace TagCloud.TextHandlers.Filters
{
    public class TextFilter : ITextFilter
    {
        private readonly List<Func<string, bool>> filters = new();

        public TextFilter()
        {
        }

        public TextFilter(params IFilter[] filters)
        {
            foreach (var filter in filters)
            {
                Using(filter.IsSuit);
            }
        }

        public TextFilter Using(Func<string, bool> filter)
        {
            filters.Add(filter);
            return this;
        }

        public IEnumerable<string> Filter(IEnumerable<string> words)
        {
            return words.Where(w => filters.All(f => f(w)));
        }
    }
}