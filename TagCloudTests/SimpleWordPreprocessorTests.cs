using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.BoringWordsRepositories;
using TagCloud.Readers;
using TagCloud.WordPreprocessors;

namespace TagCloudTests
{
    public class SimpleWordPreprocessorTests
    {
        private IReader wordsReader;
        private IWordPreprocessor wordPreprocessor;

        [SetUp]
        public void CreateWords()
        {
            wordsReader = new SingleWordInRowTextFileReader("aboutKonturWords.txt");
        }

        [Test]
        public void SimpleWordPreprocessor_GetPreprocessedWords_ShouldReturnСonvertWordsToLowerCase()
        {
            var words = wordsReader.ReadWords();
            var preprocessedWords = GetPreprocessedWords();

            words.Should().Contain(word => word.Any(c => char.IsUpper(c)));
            preprocessedWords.Should().NotContain(word => word.Any(c => char.IsUpper(c)));
        }


        [Test]
        public void SimpleWordPreprocessor_GetPreprocessedWords_ShouldReturnRemoveBoringWordsFromWords()
        {
            var words = wordsReader.ReadWords();
            var boringWords = new TextFileBoringWordsStorage().GetBoringWords();
            var preprocessedWords = GetPreprocessedWords();

            words.Should().Contain(word => boringWords.Contains(word));
            preprocessedWords.Should().NotContain(word => boringWords.Contains(word));
        }

        private IEnumerable<string> GetPreprocessedWords()
        {
            wordPreprocessor = new SimpleWordPreprocessor(wordsReader, new TextFileBoringWordsStorage());
            var preprocessedWords = wordPreprocessor.GetPreprocessedWords();
            return preprocessedWords;
        }
    }
}
