using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Features.AttributeFilters;
using TagsCloudVisualization.WordReaders;

namespace TagsCloudVisualization
{
    public class WordsStatistics : IWordsStatistics
    {
        private readonly IDictionary<string, int> statistics = new Dictionary<string, int>();
        private readonly IWordReader reader;

        public WordsStatistics([KeyFilter("FullProcessed")]IWordReader reader)
        {
            this.reader = reader;
        }

        public void Load()
        {
            while (reader.HasWord())
                AddWord(reader.Read());
        }

        public void AddWord(string word)
        {
            if (word == null) throw new ArgumentNullException(nameof(word));
            if (string.IsNullOrWhiteSpace(word)) return;
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