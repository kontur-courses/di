using System.Collections.Generic;
using TagCloudVisualization;

namespace TagCloudCreation
{
    public interface ITagCloudStatsGenerator
    {
        List<WordInfo> GenerateStats(IEnumerable<string> words);
    }
}
