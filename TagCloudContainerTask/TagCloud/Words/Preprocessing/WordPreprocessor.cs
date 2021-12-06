using System.Collections.Generic;
using System.Linq;

namespace TagCloud.Words.Preprocessing
{
    public class WordPreprocessor
    {
        public List<string> LeadingWordsToLower(List<string> words)
        {
            return words.Select(word => word.ToLower()).ToList();
        }
    }
}