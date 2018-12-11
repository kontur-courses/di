using System.Collections.Generic;

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

        public string PrepareWord(string word, TagCloudCreationOptions _) => boringWords.Contains(word) ? null : word;
    }
}
