using System.Collections.Generic;
using System.Linq;

namespace TagsCloud.TextProcessing.TextFilters
{
    public class FiltersFactory : IFiltersFactory
    {
        private readonly Dictionary<string, ITextFilter> textFilters;

        public FiltersFactory(IEnumerable<ITextFilter> textFilters)
        {
            this.textFilters = textFilters.ToDictionary(filter => filter.Name);
        }

        public IEnumerable<string> ApplyFilters(IEnumerable<string> text, string[] filterNames)
        {
            return text.Where(word => filterNames.All(name => textFilters[name].CanTake(word)));
        }

        public IEnumerable<string> GetFilerNames() => textFilters.Select(pair => pair.Key);
    }
}
