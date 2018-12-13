using System.Collections.Generic;
using TagsCloudContainer.Formatting;
using TagsCloudContainer.UI;

namespace TagsCloudContainer.Filtering
{
    public interface IFiltersFactory
    {
        List<IWordsFilter> CreateFilters(IUI ui);
    }
}