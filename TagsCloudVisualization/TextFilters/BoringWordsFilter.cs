using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.TextFilters
{
    public class BoringWordsFilter : ITextFilter
    {
        private readonly HashSet<string> boringWords;

        public BoringWordsFilter(IEnumerable<string> boringWords)
        {
            this.boringWords = boringWords.ToHashSet();
        }

        public IEnumerable<string> FilterWords(IEnumerable<string> words)
        {
            return words.Where(word => !boringWords.Contains(word));
        }
    }
}