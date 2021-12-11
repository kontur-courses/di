using System.Collections.Generic;
using System.Linq;

namespace TagCloud.WordsPreprocessing
{
    public class WordsPreprocessor
    {
        public IEnumerable<string> LeadingWordsToLowerCase(IEnumerable<string> words)
        {
            return words.Select(word => word.ToLower());
        }
    }
}