using TagsCloudContainer.Interfaces;
using NUnit.Framework;
using FluentAssertions;
using TagsCloudContainer.TagsCloud;

namespace TagsCloudContainerTests
{
    [TestFixture]
    public class WordPreprocessorTests
    {
        private IPreprocessor preprocessor;
        [SetUp]
        public void SetUp()
        {
            preprocessor = new WordPreprocessor(new List<string>());
        }

        [Test]
        public void WithCommonWords_RemovesCommonWords()
        {
            var words = new[] { "the", "quick", "a", "fox", "jumps" };

            var processedWords = preprocessor.Process(words, "src/boring_words.txt");

            processedWords.Should().HaveCount(3);
            processedWords.Should().NotContain("a");
            processedWords.Should().NotContain("the");
        }

        [Test]
        public void WithUpperCase_ConvertsToLowercase()
        {
            var words = new[] { "Word1", "Word2", "Word3" };

            var processedWords = preprocessor.Process(words, "src/boring_words.txt");

            processedWords.Should().BeEquivalentTo("word1", "word2", "word3");
        }

        [Test]
        public void WithEmptyWordsList_ReturnsEmptyList()
        {
            var words = new List<string>();

            var processedWords = preprocessor.Process(words, "src/boring_words.txt");

            processedWords.Should().BeEmpty();
        }

        [Test]
        public void WithEmptyBoringWordsFile_ReturnsOriginalWords()
        {
            var words = new[] { "apple", "orange", "banana" };

            var processedWords = preprocessor.Process(words, "src/empty.txt");

            processedWords.Should().BeEquivalentTo(words);
        }
    }

}

