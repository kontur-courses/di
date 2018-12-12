using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.Preprocessing
{
    public class FrequencyCounter
    {
        private IEnumerable<WordInfo> OrderWordFrequencies(Dictionary<string, int> frequencies)
        {
            return frequencies
                .OrderByDescending(kv => kv.Value)
                .Select(kv => new WordInfo
                {
                    Word = kv.Key,
                    Frequency = kv.Value
                });
        }

        public IEnumerable<WordInfo> CountWordFrequencies(IEnumerable<string> words)
        {
            if (words == null)
                throw new ArgumentNullException(nameof(words));
            var wordsFrequencies = new Dictionary<string, int>();
            foreach (var word in words)
            {
                wordsFrequencies.TryGetValue(word, out var value);
                wordsFrequencies[word] = value + 1;
            }

            return OrderWordFrequencies(wordsFrequencies);
        }
    }
}