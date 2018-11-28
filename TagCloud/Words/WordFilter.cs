using System.Collections.Generic;
using System.Linq;

namespace TagCloud.Words
{
    public class WordFilter
    {
        private readonly ExcludingWords excludingWords;
        
        public WordFilter(ExcludingWords excludingWords)
        {
            this.excludingWords = excludingWords;
        }
        
        public IEnumerable<string> Filter(IEnumerable<string> words)
        {
            return words.Where(w => !excludingWords.Contains(w));
        }
    }
} 