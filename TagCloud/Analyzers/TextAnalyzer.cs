using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud.Analyzers
{
    class TextAnalyzer : ITextAnalyzer
    {
        private readonly HashSet<string> excludedWords;

        public TextAnalyzer(HashSet<string> excludedWords)
        {
            this.excludedWords = excludedWords;
        }

        public IEnumerable<string> Analyze(string[] words)
        {
            return words.Where(w => !excludedWords.Contains(w))
                .Select(w => w.ToLowerInvariant());
        }
    }
}
