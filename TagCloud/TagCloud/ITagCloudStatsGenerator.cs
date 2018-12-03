using System.Collections.Generic;

namespace TagCloud
{
    internal interface ITagCloudStatsGenerator
    {
        IEnumerable<WordInfo> GenerateStats(IEnumerable<string> words);
    }
}