using System;
using System.Collections.Generic;
using System.Linq;

namespace TagCloud
{
    internal class TagCloudStatsGenerator : ITagCloudStatsGenerator
    {
        public IEnumerable<WordInfo> GenerateStats(IEnumerable<string> words)
        {
            words = words.ToList();
            return words.
                 GroupBy(w=>w).Select(g=>new WordInfo(g.Key, g.Count()));
        }
    }
}