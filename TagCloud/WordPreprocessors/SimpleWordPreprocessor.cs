using System.Collections.Generic;
using System.Linq;
using TagCloud.BoringWordsStorage;
using TagCloud.Readers;

namespace TagCloud.WordPreprocessors
{
    public class SimpleWordPreprocessor : IWordPreprocessor
    {
        private readonly IEnumerable<string> words;
        private readonly HashSet<string> boringWords;

        public SimpleWordPreprocessor(IReader wordsReader, IBoringWordsStorage boringWordsStorage)
        {
            words = wordsReader.ReadWords();
            boringWords = boringWordsStorage.GetBoringWords();
        }

        public IEnumerable<string> GetPreprocessedWords()
        {
            var lowerCaseWords = СonvertToLowerCase(words);
            var preprocessedWords = RemoveBoringWordsFrom(lowerCaseWords);
            return preprocessedWords;
        }

        private IEnumerable<string> СonvertToLowerCase(IEnumerable<string> words) =>
            words.Select(word => word.ToLower());

        private IEnumerable<string> RemoveBoringWordsFrom(IEnumerable<string> words) =>
            words.Where(w => !boringWords.Contains(w));

    }
}
