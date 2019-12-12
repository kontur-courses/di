using System.Collections.Generic;

namespace TagsCloudTextProcessing.Filters
{
    public interface IWordsFilter
    {
        IEnumerable<string> Filter(IEnumerable<string> inputWords);
    }
}