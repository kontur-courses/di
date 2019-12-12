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
                .Select(s => s.ToLower())
                .GroupBy(g => g)
                .Select(s => new ValueTuple<string, int>(s.Key, s.Count()))
                .ToDictionary(x => x.Item1, x => x.Item2);
        }
    }
}