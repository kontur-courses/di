using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.Common.WordFilters
{
    public class CustomFilter : IWordFilter
    {
        private readonly HashSet<string> excludeWords;

        public CustomFilter(IEnumerable<string> words)
        {
            excludeWords = new HashSet<string>();
            foreach (var word in words
                .Select(word => word.Trim().ToLower())
                .Where(word => !string.IsNullOrEmpty(word)))
                excludeWords.Add(word);
        }

        public IEnumerable<string> Filter(IEnumerable<string> words)
        {
            return words.Where(word => !excludeWords.Contains(word));
        }
    }
}