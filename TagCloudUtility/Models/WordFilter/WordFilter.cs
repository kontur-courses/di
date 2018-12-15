using System;
using System.Collections.Generic;
using System.Linq;

namespace TagCloud.Utility.Models.WordFilter
{
    public class WordFilter : IWordFilter
    {
        protected readonly HashSet<string> stopWords;
        protected readonly int minimalWordLength;
        protected readonly Func<string, bool> selector;
        protected readonly Func<string, string> convertor;

        public WordFilter(
            IEnumerable<string> stopWords,
            int minimalWordLength = 3,
            Func<string, bool> selector = null,
            Func<string, string> convertor = null)
        {
            this.stopWords = new HashSet<string>();
            this.stopWords.UnionWith(stopWords);
            this.minimalWordLength = minimalWordLength;
            this.selector = selector ?? (w => true);
            this.convertor = convertor ?? (w => w);
        }

        public string[] FilterWords(string[] words)
        {
            return words
                .Select(word => word.ToLower())
                .Where(word => word.Length > minimalWordLength
                               && !stopWords.Contains(word)
                               && selector(word))
                .Select(word => convertor(word))
                .ToArray();

        }

        public void Add(string stopWord)
        {
            if (!stopWords.Contains(stopWord) && stopWord.Length >= minimalWordLength)
                stopWords.Add(stopWord);
        }
    }
}