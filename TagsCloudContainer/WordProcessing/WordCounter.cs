using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.WordProcessing
{
    public class WordCounter : IWordStatisticsCalculator
    {
        public IEnumerable<WordData> CalculateStatistics(IEnumerable<string> words)
        {
            var counter = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (!counter.ContainsKey(word))
                    counter[word] = 0;
                counter[word] += 1;
            }

            return counter.Select(w => new WordData(w.Key, w.Value));
        }
    }
}