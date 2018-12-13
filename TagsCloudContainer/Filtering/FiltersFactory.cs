using System.Collections.Generic;
using TagsCloudContainer.Reading;
using TagsCloudContainer.UI;

namespace TagsCloudContainer.Filtering
{
    public class FiltersFactory : IFiltersFactory
    {
        public List<IWordsFilter> CreateFilters(IUI ui)
        {
            var result = new List<IWordsFilter>
            {
                new BlacklistWordsFilter(new HashSet<string>(new TxtWordsReader().ReadWords(ui.BlacklistPath)))
            };
            return result;
        }
    }
}