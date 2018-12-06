using System.Collections.Generic;

namespace TagsCloudContainer.Interfaces
{
    public interface IWordCounter
    {
        IDictionary<string, int> GetWordsFrequency();
    }
}