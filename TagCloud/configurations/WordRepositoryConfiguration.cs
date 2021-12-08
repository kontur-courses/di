using System.Collections.Generic;
using System.Linq;
using TagCloud.selectors;

namespace TagCloud.configurations
{
    public class WordRepositoryConfiguration : IWordRepositoryConfiguration
    {
        private readonly List<IWordHandler> handlers;
        private readonly List<IWordFilter> filters;

        public WordRepositoryConfiguration(List<IWordHandler> handlers, List<IWordFilter> filters)
        {
            this.handlers = handlers;
            this.filters = filters;
        }

        public IEnumerable<string> Handle(IEnumerable<string> source) =>
            source.Select(word => handlers.Aggregate(word, (current, handler) => handler.Handle(current)));

        public IEnumerable<string> Filter(IEnumerable<string> source) =>
            filters.Count == 0 
                ? source 
                : source.Where(word => filters.Any(filter => filter.Filter(word)));
    }
}