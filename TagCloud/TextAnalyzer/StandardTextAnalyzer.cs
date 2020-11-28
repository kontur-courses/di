using System.Collections.Generic;
using System.Linq;
using TagCloud.TextAnalyzer.WordNormalizer;

namespace TagCloud.TextAnalyzer
{
    public class StandardAnalyzer : ITextAnalyzer
    {
        private IWordNormalizer normalizer;
        
        public StandardAnalyzer(IWordNormalizer normalizer)
        {
            this.normalizer = normalizer;
        }

        public Dictionary<string, int> GetWordCounts(List<string> words)
        {
            var wordCounts = new Dictionary<string, int>();
            foreach (var normalizedWord in words.Select(word => normalizer.Normalize(word)))
            {
                if (!wordCounts.ContainsKey(normalizedWord))
                    wordCounts[normalizedWord] = 0;
                wordCounts[normalizedWord]++;
            }

            return wordCounts;
        }
    }
}