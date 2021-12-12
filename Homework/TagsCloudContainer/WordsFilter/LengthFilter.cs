using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TagsCloudContainer.WordsPreparator;

[assembly: InternalsVisibleTo("TagsCloudContainer_Tests")]

namespace TagsCloudContainer.WordsFilter
{
    public class LengthFilter : IWordsFilter
    {
        private readonly int minLength;

        internal LengthFilter(int minLength)
        {
            if (minLength < 0)
                throw new ArgumentException($"{nameof(minLength)} cannot be negative");
            this.minLength = minLength;
        }

        public LengthFilter(ITagCloudSettings settings)
        {
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