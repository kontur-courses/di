using System.Collections.Generic;
using System.Linq;

namespace TagCloud.Analyzers
{
    public class BoringWordsFilter : IWordFilter
    {
        private readonly HashSet<string> boringWords = new();

        public void AddWords(IEnumerable<string> words)
        {
            boringWords.UnionWith(words);
        }

        public IEnumerable<string> Analyze(IEnumerable<string> words)
        {
            return words.Where(w => !boringWords.Contains(w));
        }
    }
}
