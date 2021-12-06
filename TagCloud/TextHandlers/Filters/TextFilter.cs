using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagCloud.TextHandlers.Filters
{
    public class TextFilter : ITextFilter
    {
        private List<Func<string, bool>> filters = new List<Func<string, bool>>();

        public TextFilter()
        {
        }

        public TextFilter(IFilter[] filters)
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