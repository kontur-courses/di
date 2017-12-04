using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public interface IWordsAnalyzer
    {
        Dictionary<string, int> GetWordsFrequensy();
    }
}