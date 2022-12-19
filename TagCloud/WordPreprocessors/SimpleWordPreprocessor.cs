using System.Collections.Generic;
using System.Linq;
using TagCloud.BoringWordsRepositories;
using TagCloud.Readers;

namespace TagCloud.WordPreprocessors
{
    public class SimpleWordPreprocessor : IWordPreprocessor
    {
        private readonly IReader wordsReader;
        private readonly IBoringWordsStorage boringWordsStorage;
        private HashSet<string> boringWords;

        public SimpleWordPreprocessor(IReader wordsReader, IBoringWordsStorage boringWordsStorage)
        {
            this.wordsReader = wordsReader;
            this.boringWordsStorage = boringWordsStorage;
        }

        public IEnumerable<string> GetPreprocessedWords()
        {
            boringWords = boringWordsStorage.GetBoringWords();
            return wordsReader
                .ReadWords()
                .Select(word => word.ToLower())
                .Where(word => !boringWords.Contains(word));
        }
    }
}
