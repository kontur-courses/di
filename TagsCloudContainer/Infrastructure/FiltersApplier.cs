using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer
{
    public class FiltersApplier : IFiltersApplier
    {
        private IFilter[] filters;

        public FiltersApplier(Settings settings)
        {
            filters = settings.Filters;
        }

        public IEnumerable<string> ApplyFilters(IEnumerable<string> words)
        {
            foreach (var word in words)
            {
                if (filters.Select(filter => filter.Allows(word))
                    .All(result => result))
                    yield return word;
            }
        }
    }
}
