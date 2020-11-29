using System.Collections.Generic;

namespace TagCloud.WordsMetrics
{
    internal interface IWordsMetric
    {
        public Dictionary<string, double> Process(IEnumerable<string> words);
    }
}
