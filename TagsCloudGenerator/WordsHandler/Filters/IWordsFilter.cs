using System.Collections.Generic;

namespace TagsCloudGenerator.WordsHandler.Filters
{
    public interface IWordsFilter
    {
        Dictionary<string, int> Filter(Dictionary<string, int> wordToCount);
    }
}