using System.Collections.Generic;
using TagsCloudContainer.Formatting;
using TagsCloudContainer.UI;

namespace TagsCloudContainer.Filtering
{
    public class FilteringSettings
    {
        public readonly List<IWordsFilter> Filters;

        public FilteringSettings(IUI ui, IFiltersFactory filtersFactory)
        {
            Filters = filtersFactory.CreateFilters(ui);
        }

        public FilteringSettings(List<IWordsFilter> filters)
        {
            Filters = filters;
        }
    }
}