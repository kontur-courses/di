using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization
{
    public class StatisticsCollector : IStatisticsCollector
    {
        public IEnumerable<FrequentedWord> GetStatistics(IEnumerable<string> words)
        {
            var statistics = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (!statistics.ContainsKey(word))
                    statistics[word] = 0;
                statistics[word]++;
            }

            return statistics.Select(p => new FrequentedWord(p.Key, p.Value));
        }
    }
}