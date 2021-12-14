using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.WordsPreprocessors.Filters
{
    public class BoringWordsFilter : IWordsFilter
    {
        private readonly HashSet<string> boringWords;

        public BoringWordsFilter(IEnumerable<string> boringWords)
        {
            this.boringWords = boringWords
                .Select(word => word.ToLowerInvariant())
                .ToHashSet();
        }

        public IEnumerable<string> Filter(IEnumerable<string> words) =>
            words.Where(word => !string.IsNullOrWhiteSpace(word) && !IsBoring(word));

        private bool IsBoring(string word) => boringWords.Contains(word);
    }
}