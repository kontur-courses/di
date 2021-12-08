using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CloudTagContainer
{
    public class CountingWordSizer: IWordSizer
    {
        public List<SizedWord> Convert(string[] words, float fontSize)
        {
            if (fontSize <= 0) 
                throw new ArgumentException($"{nameof(fontSize)} must be positive");
            if (words == null)
                throw new ArgumentNullException($"{nameof(words)} can not be null");
            
            var wordToCount = words
                .GroupBy(x => x)
                .ToDictionary(x => x.Key, y => y.Count());

            return wordToCount.Select(kv =>
                    new SizedWord(
                        kv.Key,
                        CalculateSize(kv.Value, fontSize, kv.Key)))
                .ToList();
        }

        private Size CalculateSize(int repeatedCount, float fontSize, string word)
        {
            return new Size(
                (int) (1.5d * repeatedCount * word.Length * fontSize),
                 (int) (1.5d * repeatedCount * fontSize)
            );
        }
    }
}