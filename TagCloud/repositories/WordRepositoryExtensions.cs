using System.Collections.Generic;

namespace TagCloud.repositories
{
    public static class WordRepositoryExtensions
    {
        public static Dictionary<string, int> CalculateWordStatistics(this WordRepository wordRepository)
        {
            var statistics = new Dictionary<string, int>();
            foreach (var word in wordRepository.Get())
            {
                if (!statistics.ContainsKey(word))
                    statistics.Add(word, 0);
                statistics[word]++;
            }

            return statistics;
        }
    }
}