using System.Collections.Generic;

namespace TagsCloudContainer.Processing.Filtering
{
    public interface IWordFilter
    {
        IEnumerable<string> Filter(IEnumerable<string> words);
    }
}