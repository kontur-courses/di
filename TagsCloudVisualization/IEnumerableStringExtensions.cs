using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization
{
    public static class IEnumerableStringExtensions
    {
        public static IEnumerable<string> GetLowerCaseWords(this IEnumerable<string> words)
        {
            return words.Select(word => word.ToLower());
        }

        public static IEnumerable<string> GetFilteredWords(this IEnumerable<string> words, ITextFilter filter)
        {
            return filter.FilterWords(words);
        }
    }
}