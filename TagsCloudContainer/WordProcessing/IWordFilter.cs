using System.Collections.Generic;

namespace TagsCloudContainer.WordProcessing
{
    public interface IWordFilter
    {
        IEnumerable<string> Filter(IEnumerable<string> words);
    }
}