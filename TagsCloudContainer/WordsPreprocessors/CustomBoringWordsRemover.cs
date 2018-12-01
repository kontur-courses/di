using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.WordsPreprocessors
{
    public class CustomBoringWordsRemover : IWordsPreprocessor
    {
        private readonly HashSet<string> customBoringWords = new HashSet<string>
        {
            "острый",
            "шар",
            "волосатое",
            "стекло"
        };

        public IEnumerable<string> Preprocess(IEnumerable<string> words)
        {
            return words.Where(word => !customBoringWords.Contains(word));
        }
    }
}