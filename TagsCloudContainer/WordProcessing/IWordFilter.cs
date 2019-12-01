using System.Collections.Generic;

namespace TagsCloudContainer.WordProcessing
{
    public interface IWordFilter
    {
        IEnumerable<string> FilterWords(IEnumerable<string> words);
    }
}