using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.Core;

namespace TagsCloudVisualization.WordStatistics
{
    public class WordCountCollector : IStatisticsCollector
    {
        public IEnumerable<(WordStatistics word, int value)> GetStatistics(IEnumerable<Word> words)
        {
            var counter = new Dictionary<Word, int>();
            foreach (var word in words)
            {
                if (!counter.ContainsKey(word))
                    counter[word] = 0;
                counter[word]++;
            }

            return counter
                .Select(x => (new WordStatistics(x.Key, StatisticsType.WordCount), x.Value));
        }
    }
}