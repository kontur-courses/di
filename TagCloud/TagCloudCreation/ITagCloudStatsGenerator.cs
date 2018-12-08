using System.Collections.Generic;

namespace TagCloudCreation
{
    public interface ITagCloudStatsGenerator
    {
        List<WordInfo> GenerateStats(IEnumerable<string> words);
    }
}
