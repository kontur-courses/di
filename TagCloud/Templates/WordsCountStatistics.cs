using System.Collections.Generic;

namespace TagCloud.Templates
{
    public class WordsCountStatistics
    {
        public Dictionary<string, int> GetStatistics(IEnumerable<string> words)
        {
            var statistics = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (statistics.ContainsKey(word))
                {
                    statistics[word] += 1;
                }
                else
                {
                    statistics[word] = 1;
                }
            }

            return statistics;
        }
    }
}