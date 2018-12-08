using System;
using System.Collections.Generic;
using System.Linq;

namespace TagCloud
{
    internal interface ITagCloudStatsGenerator
    {
        List<WordInfo> GenerateStats(IEnumerable<string> words);

    }
}
