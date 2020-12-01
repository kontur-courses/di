using System.Collections.Generic;
using System.Linq;
using TagsCloud.TextProcessing.WordConfig;

namespace TagsCloud.TextProcessing.TextFilters
{
    public class FiltersApplier : IFiltersApplier
    {
        private readonly Dictionary<string, ITextFilter> textFilters;
        private readonly IWordsConfig wordsConfig;

        public FiltersApplier(IEnumerable<ITextFilter> textFilters, IWordsConfig wordsConfig)
        {
            this.textFilters = textFilters.ToDictionary(filter => filter.Name);
            this.wordsConfig = wordsConfig;
        }

        public IEnumerable<string> ApplyFilters(IEnumerable<string> text)
        {
            var filterNames = wordsConfig.FilerNames;
            return text.Where(word => filterNames.All(name => textFilters[name].CanTake(word)));
        }

        public IEnumerable<string> GetFilerNames() => textFilters.Select(pair => pair.Key);
    }
}
