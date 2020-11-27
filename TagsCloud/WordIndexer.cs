using System.Collections.Generic;
using System.Linq;

namespace TagsCloud
{
    public static class WordIndexer
    {
        private static readonly HashSet<string> BoringWords = new HashSet<string>
        {
            "я", "мы", "ты", "вы", "они", "он", "она",
            "а", "и", "но",
            "we", "i", "you", "they",
            "and", "but", "then",
        };
        
        public static Dictionary<string, int> GetWordStatistics(this IEnumerable<string> words)
        {
            var result = new Dictionary<string, int>();
            
            foreach (var word in words.PrepareWords())
            {
                if (!result.ContainsKey(word))
                    result[word] = 0;
                result[word]++;
            }

            return result;
        }

        private static IEnumerable<string> PrepareWords(this IEnumerable<string> words) =>
            words.Select(w => w.ToLower()).Where(w => !BoringWords.Contains(w));
    }
}