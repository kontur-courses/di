using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public interface IFileAnalyzer
    {
        Dictionary<string, int> GetWordsFrequensy();
    }
}