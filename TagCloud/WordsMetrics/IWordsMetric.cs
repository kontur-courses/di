using System.Collections.Generic;

namespace TagCloud.WordsMetrics
{
    public interface IWordsMetric
    {
        public Dictionary<string, double> Process(IEnumerable<string> words);
    }
}
