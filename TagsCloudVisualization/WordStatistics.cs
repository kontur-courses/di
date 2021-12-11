﻿using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.WordProcessors;

namespace TagsCloudVisualization
{
    public class WordsStatistics : IWordsStatistics
    {
        private readonly IDictionary<string, int> statistics = new Dictionary<string, int>();
        private readonly ITextProcessor textProcessor;

        public WordsStatistics(ITextProcessor textProcessor)
        {
            this.textProcessor = textProcessor;
        }

        private void AddProcessedWord(string word)
        {
            if (word == null) throw new ArgumentNullException(nameof(word));
            if (string.IsNullOrWhiteSpace(word)) return;
            statistics[word.ToLower()] = 1 + (statistics.TryGetValue(word.ToLower(), out var count) ? count : 0);
        }

        public void AddWords(IEnumerable<string> word)
        {
            foreach (var processWord in textProcessor.ProcessWords(word))
            {
                AddProcessedWord(processWord);
            }
        }

        public virtual IEnumerable<WordCount> GetStatistics(int topWordCount = -1)
        {
            if (topWordCount < 0)
                return statistics
                    .Select(WordCount.Create)
                    .OrderByDescending(wordCount => wordCount.Count)
                    .ThenBy(wordCount => wordCount.Word);
            
            return statistics
                .Select(WordCount.Create)
                .OrderByDescending(wordCount => wordCount.Count)
                .ThenBy(wordCount => wordCount.Word)
                .Take(topWordCount);
        }
    }
}