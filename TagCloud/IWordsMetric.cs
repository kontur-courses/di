using System.Collections.Generic;

namespace TagCloud
{
    internal interface IWordsMetric
    {
        public Dictionary<string, double> Process(IEnumerable<string> words);
    }
}
