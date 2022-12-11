using System.Collections.Generic;
using System.Linq;

namespace TagCloud.WordFilter
{
    public class FiltersExecutor : IFiltersExecutor
    {
        private readonly List<IWordFilter> Filters = new List<IWordFilter>();

        public FiltersExecutor(IWordFilter[] filters)
        {
            foreach (var filter in filters)
                RegisterFilter(filter);
        }

        public void RegisterFilter(IWordFilter filter)
        {
            Filters.Add(filter);
        }

        public IReadOnlyList<string> Filter(IEnumerable<string> words)
        {
            return words.Where(word => Filters.All(filter => filter.IsPermitted(word))).ToList();
        }
    }
}
 