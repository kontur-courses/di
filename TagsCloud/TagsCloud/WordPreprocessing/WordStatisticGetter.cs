using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloud.WordPreprocessing
{
    public class WordStatisticGetter : IWordAnalyzer
    {
        public Dictionary<string, int> GetWordsStatistics(IEnumerable<string> words)
        {
            return words
                .GroupBy(g => g)
                .ToDictionary(x => x.Key, x => x.Count());
        }
    }
}