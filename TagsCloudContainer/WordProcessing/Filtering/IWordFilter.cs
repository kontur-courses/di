using System.Collections.Generic;

namespace TagsCloudContainer.WordProcessing.Filtering
{
    public interface IWordFilter
    {
        IEnumerable<string> FilterWords(IEnumerable<string> words);
    }
}