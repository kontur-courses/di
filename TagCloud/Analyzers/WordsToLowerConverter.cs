using System.Collections.Generic;
using System.Linq;

namespace TagCloud.Analyzers
{
    public class WordsToLowerConverter : IWordConverter
    {
        public IEnumerable<string> Convert(IEnumerable<string> words)
        {
            return words.Select(w => w.ToLower());
        }
    }
}
