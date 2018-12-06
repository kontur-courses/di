using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Preprocessing;

namespace TagsCloudContainer.Tests
{
    public class WordPreprocessor_Should
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
            var preprocessor = new WordsPreprocessor(settings);

            var result = preprocessor.CountWordFrequencies(words);

            result.Select(info => info.Word).Should().NotContain(wordsToExclude);
        }

        [TestCase(4)]
        [TestCase(2)]
        public void ExcludeBoringWords(int length)
        {
            var settings = new WordsPreprocessorSettings
            {
                BoringWordsLength = length
            };
            var preprocessor = new WordsPreprocessor(settings);

            var result = preprocessor.CountWordFrequencies(words);

            result.Select(info => info.Word).Should().NotContain(str => str.Length < length);
        }

        [Test]
        public void OrderWordsByFrequency_DyDescending()
        {
            var settings = new WordsPreprocessorSettings();
            var preprocessor = new WordsPreprocessor(settings);

            var result = preprocessor.CountWordFrequencies(words).Select(info => info.Frequency);

            var previous = int.MaxValue;
            foreach (var frequency in result)
            {
                frequency.Should().BeLessOrEqualTo(previous);
                previous = frequency;
            }
        }

        [Test]
        public void CountWordsFrequency()
        {
            var settings = new WordsPreprocessorSettings();
            var preprocessor = new WordsPreprocessor(settings);

            var result = preprocessor.CountWordFrequencies(words);

            foreach (var info in result)
            {
                var frequency = words.Count(word => word == info.Word);
                info.Frequency.Should().Be(frequency);
            }
        }

        [Test]
        public void ThrowException_WhenWordsAreNull()
        {
            var settings = new WordsPreprocessorSettings();
            var preprocessor = new WordsPreprocessor(settings);

            Action runner = () => preprocessor.CountWordFrequencies(null);
            runner.Should().Throw<ArgumentNullException>();
        }
    }
}