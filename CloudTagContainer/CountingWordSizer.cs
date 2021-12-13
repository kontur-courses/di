using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Visualization
{
    public class CountingWordSizer : IWordSizer
    {
        public List<SizedWord> Convert(string[] words, float fontSize)
        {
            if (fontSize <= 0)
                throw new ArgumentException($"{nameof(fontSize)} must be positive");
            if (words == null)
                throw new ArgumentNullException($"{nameof(words)} can not be null");

            var wordToRepeatingCount = words
                .GroupBy(x => x)
                .ToDictionary(x => x.Key, y => y.Count());

            return wordToRepeatingCount.Select(kv =>
                    new SizedWord(
                        kv.Key,
                        CalculateSize(kv.Value, fontSize, kv.Key)))
                .ToList();
        }

        private Size CalculateSize(int repeatedCount, float fontSize, string word)
            => new((int) (repeatedCount * word.Length * fontSize),
                (int) (repeatedCount * fontSize)
            );
    }
}