using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloud.TextProcessing.WordsConfig;

namespace TagsCloud.TextProcessing.TextFilters
{
    public class FiltersApplier : IFiltersApplier
    {
        private readonly Dictionary<string, Func<ITextFilter>> textFilters;
        private readonly WordConfig wordsConfig;

        public FiltersApplier(WordConfig wordsConfig)
        {
            textFilters = new Dictionary<string, Func<ITextFilter>>();
            this.wordsConfig = wordsConfig;
        }

        public IEnumerable<string> ApplyFilters(IEnumerable<string> text)
        {
            var filterNames = wordsConfig.FilerNames;
            return text.Where(word => filterNames.All(name => textFilters[name]().CanTake(word)));
        }

        public IEnumerable<string> GetFilerNames() => textFilters.Select(pair => pair.Key);

        public IFiltersApplier Register(string filterName, Func<ITextFilter> textFilter)
        {
            textFilters[filterName] = textFilter;
            return this;
        }
    }
}
