using System.Collections.Generic;

namespace TagsCloudContainer.Filtering
{
    public interface IWordsFilter
    {
        List<string> Filter(IEnumerable<string> words);
    }
}