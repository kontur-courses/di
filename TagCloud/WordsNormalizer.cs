using System.Collections.Generic;
using System.Linq;

namespace TagCloud
{
    public class WordsNormalizer : IWordsNormalizer
    {
        private readonly HashSet<string> boringWords;

        public WordsNormalizer(HashSet<string> boringWords = null)
        {
            this.boringWords = boringWords ?? new HashSet<string>();
        }

        public List<string> NormalizeWords(List<string> words) =>
            words.Where(x => !boringWords.Contains(x)).Select(x => x.ToLower()).ToList();
    }
}