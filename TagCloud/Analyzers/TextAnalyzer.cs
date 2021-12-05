using System.Collections.Generic;
using System.Linq;

namespace TagCloud.Analyzers
{
    public class TextAnalyzer : ITextAnalyzer
    {
        public IEnumerable<string> Analyze(string[] words, HashSet<string> wordsToExclude)
        {
            return words.Where(w => !wordsToExclude.Contains(w))
                .Select(w => w.ToLowerInvariant());
        }
    }
}
