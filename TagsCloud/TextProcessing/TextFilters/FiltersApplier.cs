using System.Collections.Generic;
using System.Linq;
using TagsCloud.TextProcessing.WordConfig;

namespace TagsCloud.TextProcessing.TextFilters
{
    public class FiltersApplier : IFiltersApplier
    {
        private readonly Dictionary<string, ITextFilter> textFilters;
        private readonly IWordsConfig wordsConfig;

        public FiltersApplier(IWordsConfig wordsConfig)
        {
            textFilters = new Dictionary<string, ITextFilter>();
            this.wordsConfig = wordsConfig;
        }

        public IEnumerable<string> ApplyFilters(IEnumerable<string> text)
        {
            var filterNames = wordsConfig.FilerNames;
            return text.Where(word => filterNames.All(name => textFilters[name].CanTake(word)));
        }

        public IEnumerable<string> GetFilerNames() => textFilters.Select(pair => pair.Key);

        public void Register(string filterName, ITextFilter textFilter) =>
                                                        textFilters[filterName] = textFilter;
    }
}
