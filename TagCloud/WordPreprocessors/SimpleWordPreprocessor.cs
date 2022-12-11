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
            var lowerCaseWords = СonvertToLowerCase(wordsReader.ReadWords());
            var preprocessedWords = RemoveBoringWordsFrom(lowerCaseWords);
            return preprocessedWords;
        }

        private IEnumerable<string> СonvertToLowerCase(IEnumerable<string> words) =>
            words.Select(word => word.ToLower());

        private IEnumerable<string> RemoveBoringWordsFrom(IEnumerable<string> words) =>
            words.Where(word => !boringWords.Contains(word));

    }
}
