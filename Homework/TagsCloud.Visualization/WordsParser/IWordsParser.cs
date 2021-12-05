using System.Collections.Generic;

namespace TagsCloud.Visualization.WordsParser
{
    public interface IWordsParser
    {
        Dictionary<string, int> CountWordsFrequency(string text);
    }
}