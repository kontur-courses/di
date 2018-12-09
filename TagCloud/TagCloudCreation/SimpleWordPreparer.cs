using System.Collections.Generic;

namespace TagCloudCreation
{
    public class SimpleWordPreparer : IWordPreparer
    {
        private readonly HashSet<string> boringWords = new HashSet<string>
        {
            "is", "are", "he", "she",
            "it", "they", "do", "does",
            "don't", "doesn't", "not", "aren't",
            "isn't", "the", "a", "and",
            "an"
        };

        public WordInfo PrepareWords(WordInfo stat) => boringWords.Contains(stat.Word) ? null : stat;
    }
}
