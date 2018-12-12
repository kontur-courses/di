using System;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Preprocessing;

namespace TagsCloudContainer.Tests
{
    public class BoringWordsExcluder_Should
    {
        private string[] words = {
            "something",
            "foo",
            "shpora",
            "something",
            "kontur",
            "shpora",
            "bar",
            "c-diez",
            "foo",
            "shpora",
            "foo",
            "a",
            "in",
            "o",
            "I",
            "of",
            "ye"
        };

        [TestCase("foo", "bar")]
        [TestCase("shpora", "something", "foo")]
        public void ExcludeGivenWords(params string[] wordsToExclude)
        {
            var settings = new WordsPreprocessorSettings
            {
                ExcludedWords = wordsToExclude
            };
            var preprocessor = new BoringWordsExcluder(settings);

            var result = preprocessor.Process(words);

            result.Should().NotContain(wordsToExclude);
        }

        [TestCase(4)]
        [TestCase(2)]
        public void ExcludeBoringWords(int length)
        {
            var settings = new WordsPreprocessorSettings
            {
                BoringWordsLength = length
            };
            var preprocessor = new BoringWordsExcluder(settings);

            var result = preprocessor.Process(words);

            result.Should().NotContain(str => str.Length < length);
        }

        [Test]
        public void ThrowException_WhenWordsAreNull()
        {
            var settings = new WordsPreprocessorSettings();
            var preprocessor = new BoringWordsExcluder(settings);

            Action runner = () => preprocessor.Process(null);
            runner.Should().Throw<ArgumentNullException>();
        }
    }
}