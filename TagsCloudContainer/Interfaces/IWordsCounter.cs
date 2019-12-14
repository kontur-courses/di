using System.Collections.Generic;

namespace TagsCloudContainer.Interfaces
{
    public interface IWordsCounter
    {
        Dictionary<string, int> CountWords(IEnumerable<string> arr);
    }
}