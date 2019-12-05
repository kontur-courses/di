using System.Collections.Generic;

namespace TagsCloudGenerator.WordsHandler
{
    public interface IWordsFilter
    {
        Dictionary<string, int> Filter(Dictionary<string, int> wordToCount);
    }
}