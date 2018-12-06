using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer
{
    public class SimplePreprocessor : IPreprocessor
    {
        private readonly IEnumerable<string> boringWords = new List<string>
        {
            "a",
            "the",
            "with",
            "in",
            "of",
            "at",
            "from",
            "into",
            "he",
            "i",
            "she",
            "we",
            "they",
            "we"
        };

        private IEnumerable<string> Words { get; set; }

        public SimplePreprocessor(IWordsReader wordsReader)
        {
            Words = wordsReader.GetWords();
        }

        public IEnumerable<string> Process()
        {
            return Words.Select(word => word.ToLower())
                .Where(lowerCaseWord => !boringWords.Contains(lowerCaseWord));
        }
    }
}