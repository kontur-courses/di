using System.Collections.Generic;
using System.Linq;

namespace TagCloud.TextAnalyzer.WordFilters
{
    public class BoringWordFilter : IWordFilter
    {
        private HashSet<string> boringWords;
        
        public BoringWordFilter(IReadOnlyCollection<string> boringWords)
        {
            this.boringWords = boringWords.ToHashSet();
        }

        public bool IsWordToExclude(string word)
        {
            return boringWords.Contains(word);
        }
    }
}