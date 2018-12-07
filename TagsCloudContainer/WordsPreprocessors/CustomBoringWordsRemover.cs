using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.WordsPreprocessors
{
    public class CustomBoringWordsRemover : IWordsPreprocessor
    {
        private readonly HashSet<string> customBoringWords;

        public CustomBoringWordsRemover(IEnumerable<string> customBoringWords)
        {
            this.customBoringWords = new HashSet<string>(customBoringWords);
        }

        public IEnumerable<string> Preprocess(IEnumerable<string> words)
        {
            return words.Where(word => !customBoringWords.Contains(word));
        }
    }
}