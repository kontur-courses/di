using System.Collections.Generic;
using TagCloudVisualization;

namespace TagCloudCreation
{
    public class FixedBoringWordsRemover : IWordPreparer
    {
        private readonly HashSet<string> boringWords = new HashSet<string>
        {
            "is", "are", "he", "she",
            "it", "they", "do", "does",
            "don't", "doesn't", "not", "aren't",
            "isn't", "the", "a", "and",
            "an"
        };

        public WordInfo PrepareWord(WordInfo stat, TagCloudCreationOptions _) => boringWords.Contains(stat.Word) ? null : stat;
    }
}
