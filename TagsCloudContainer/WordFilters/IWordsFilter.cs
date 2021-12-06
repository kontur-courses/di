using System.Collections.Generic;

namespace TagsCloudContainer.WordFilters
{
    public interface IWordsFilter
    {
        IEnumerable<string> Filter(IEnumerable<string> words);
    }
}