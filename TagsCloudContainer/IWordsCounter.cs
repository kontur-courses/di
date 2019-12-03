using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface IWordsCounter
    {
        IDictionary<string, int> CountWords();
    }
}