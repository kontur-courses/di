using System.Collections.Generic;

namespace TagsCloudContainer.WordsCounter
{
    public interface IWordsCounter
    {
        IDictionary<string, int> GetWordsFrequency(IEnumerable<string> words);
    }
}