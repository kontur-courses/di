using System.Collections.Generic;

namespace TagsCloudContainer.Filters
{
    public interface IWordsFilter
    {
        List<string> Filter(List<string> words);
    }
}