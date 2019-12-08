using System.Collections.Generic;
using System.Linq;

namespace TagCloudContainer
{
    public static class WordCounter
    {
        public static IReadOnlyDictionary<string, int> CreateWordOccurrencesDictionary(IEnumerable<string> words)
        {
            return words.GroupBy(s => s.ToLower()).ToDictionary(g => g.Key, g => g.Count());
        }
    }
}