using System;
using System.Collections.Generic;
using System.Linq;


namespace TagsCloudVisualization.Utils
{
    public class StatisticsCalculator
    {
        public Statistics CalculateStatistics(IEnumerable<string> words)
        {
            var allWordsCount = 0;
            var wordsCounts = new Dictionary<string, int>();

            foreach (var word in words)
            {
                if (!wordsCounts.ContainsKey(word))
                    wordsCounts[word] = 0;
                wordsCounts[word]++;
                allWordsCount++;
            }

            return new Statistics(CreateOrderedWordsStatistics(wordsCounts), allWordsCount);
        }

        private IReadOnlyList<WordStatistics> CreateOrderedWordsStatistics(Dictionary<string, int> wordsCounts)
        {
            return wordsCounts.Keys
                .Select(key => new WordStatistics(key, wordsCounts[key]))
                .OrderByDescending(stat => stat.Count)
                .ToList();
        }
    }
}
