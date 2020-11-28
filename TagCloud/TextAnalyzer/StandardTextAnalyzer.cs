using System;
using System.Collections.Generic;
using System.Linq;
using TagCloud.TextAnalyzer.WordNormalizer;

namespace TagCloud.TextAnalyzer
{
    public class StandardAnalyzer : ITextAnalyzer
    {
        private IWordNormalizer normalizer;
        private HashSet<string> bannedWords = new HashSet<string>();
        
        public StandardAnalyzer(IWordNormalizer normalizer, HashSet<string> bannedWords = null)
        {
            this.normalizer = normalizer;
            if (bannedWords != null)
                this.bannedWords = bannedWords;
        }

        public Dictionary<string, int> GetWordsCounts(List<string> words)
        {
            var wordCounts = new Dictionary<string, int>();
            foreach (var normalizedWord in words.Select(word => normalizer.Normalize(word)))
            {
                if (bannedWords.Contains(normalizedWord))
                    continue;
                
                if (!wordCounts.ContainsKey(normalizedWord))
                    wordCounts[normalizedWord] = 0;
                wordCounts[normalizedWord]++;
            }

            return wordCounts;
        }
    }
}