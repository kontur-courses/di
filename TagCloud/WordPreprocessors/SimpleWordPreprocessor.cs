using System.Collections.Generic;
using System.Linq;
using TagCloud.BoringWordsRepositories;
using TagCloud.Readers;

namespace TagCloud.WordPreprocessors
{
    public class SimpleWordPreprocessor : IWordPreprocessor
    {
        private readonly HashSet<string> boringWords;

        public SimpleWordPreprocessor(IBoringWordsStorage boringWordsStorage)
        {
            boringWords = boringWordsStorage.GetBoringWords();
        }

        public IEnumerable<string> GetPreprocessedWords(IReader wordsReader)
        {
            var lowerCaseWords = СonvertToLowerCase(wordsReader.ReadWords());
            var preprocessedWords = RemoveBoringWordsFrom(lowerCaseWords);
            return preprocessedWords;
        }

        private IEnumerable<string> СonvertToLowerCase(IEnumerable<string> words) =>
            words.Select(word => word.ToLower());

        private IEnumerable<string> RemoveBoringWordsFrom(IEnumerable<string> words) =>
            words.Where(w => !boringWords.Contains(w));

    }
}
