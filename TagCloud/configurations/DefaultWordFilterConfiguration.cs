using System.Collections.Generic;
using System.Linq;
using TagCloud.filters;

namespace TagCloud.configurations
{
    public class DefaultWordFilterConfiguration : IWordFilterConfiguration
    {
        private readonly List<IWordFilter> filters;

        public DefaultWordFilterConfiguration(List<IWordFilter> filters)
        {
            this.filters = filters;
        }

        public IEnumerable<string> Filter(IEnumerable<string> source) =>
            source.Select(word => filters.Aggregate(word, (current, filter) => filter.Filter(current)));
    }
}