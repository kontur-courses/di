using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.WordsConverters;

namespace TagsCloudContainer.WordsFilter
{
    public class LengthFilter : IWordsFilter
    {
        private readonly int minLength;

        public LengthFilter(int minLength)
        {
            if (minLength < 0)
                throw new ArgumentException($"{nameof(minLength)} cannot be negative");
            this.minLength = minLength;
        }

        public ICollection<WordInfo> Filter(ICollection<WordInfo> words)
        {
            return words
                .Where(word => word.Lemma.Length >= minLength)
                .ToArray();
        }
    }
}