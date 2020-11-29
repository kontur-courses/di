using System.Collections.Generic;

namespace TagCloud
{
    internal class CountWordMetric : IWordsMetric
    {
        public Dictionary<string, double> Process(IEnumerable<string> words)
        {
            var result = new Dictionary<string, double>();
            foreach (var word in words)
            {
                if (!result.ContainsKey(word))
                    result[word] = 0;
                result[word]++;
            }
            return result;
        }
    }
}
