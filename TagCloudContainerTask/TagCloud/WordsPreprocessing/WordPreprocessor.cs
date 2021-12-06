using System.Collections.Generic;
using System.Linq;

namespace TagCloud.WordsPreprocessing
{
    public class WordPreprocessor
    {
        public List<string> LeadingWordsToLower(List<string> words)
        {
            return words.Select(word => word.ToLower()).ToList();
        }
    }
}