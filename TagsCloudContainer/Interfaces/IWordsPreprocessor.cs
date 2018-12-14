using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface IWordsPreprocessor
    {
        Dictionary<string, int> GetWordsFrequency();
    }
}
