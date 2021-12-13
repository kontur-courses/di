using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Visualization
{
    public class CountingWordSizer : IWordSizer
    {
        public List<SizedWord> Convert(string[] words, float maxFontSize)
        {
            if (maxFontSize <= 0)
                throw new ArgumentException($"{nameof(maxFontSize)} must be positive");
            if (words == null)
                throw new ArgumentNullException($"{nameof(words)} can not be null");

            var wordToFrequency = CalculateWordsFrequency(words);
            
            return wordToFrequency.Select(kv =>
                    new SizedWord(
                        kv.Key,
                        maxFontSize * kv.Value))
                .ToList();
        }

        private static Dictionary<string, float> CalculateWordsFrequency(string[] words)
        {
            return words
                .GroupBy(x => x)
                .ToDictionary(x => x.Key,
                    y => (float) y.Count() / words.Length);
        }
    }
}