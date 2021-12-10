using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.WordsPreparator;

namespace TagsCloudContainer.WordsFilter
{
    public class LengthFilter : IWordsFilter
    {
        private readonly int minLength;
        
        public FilterType FilterType => FilterType.Length;

        public LengthFilter(int minLength)
        {
            this.minLength = minLength;
        }

        public ICollection<WordInfo> Filter(ICollection<WordInfo> words) =>
            words
                .Where(word => word.Lemma.Length >= minLength)
                .ToArray();
    }
}