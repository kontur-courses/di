using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization
{
    public class WordsStatistics : IWordsStatistics
    {
        private readonly IDictionary<string, int> statistics = new Dictionary<string, int>();

        public virtual void AddWord(string word)
        {
            if (word == null) throw new ArgumentNullException(nameof(word));
            if (string.IsNullOrWhiteSpace(word)) return;
            if (word.Length > 10)
                word = word.Substring(0, 10);
            statistics[word.ToLower()] = 1 + (statistics.TryGetValue(word.ToLower(), out var count) ? count : 0);
        }

        public virtual IEnumerable<WordCount> GetStatistics()
        {
            return statistics
                .Select(WordCount.Create)
                .OrderByDescending(wordCount => wordCount.Count)
                .ThenBy(wordCount => wordCount.Word);
        }
    }
}