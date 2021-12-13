using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.WordsPreparator;

namespace TagsCloudContainer.WordsFilter
{
    public class LengthFilter : IWordsFilter
    {
        private readonly int minLength;


        public LengthFilter(ITagCloudSettings settings)
        {
            if (settings.MinWordLength < 0)
                throw new ArgumentException($"Setting {nameof(settings.MinWordLength)} can't be negative");
            minLength = settings.MinWordLength;
        }

        public ICollection<WordInfo> Filter(ICollection<WordInfo> words)
        {
            return words
                .Where(word => word.Lemma.Length >= minLength)
                .ToArray();
        }
    }
}