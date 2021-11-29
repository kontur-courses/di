using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Preprocessing;

namespace TagsCloudContainer.Tests
{
    public class WordsPreprocessorTests
    {
        private WordsPreprocessor preprocessor;

        [SetUp]
        public void SetUp()
        {
            preprocessor = new WordsPreprocessor(new WordInfoParser());
        }

        [Test]
        public void Constructor_WithNull_ThrowsException() =>
            Assert.That(() => new WordsPreprocessor(null), Throws.InstanceOf<ArgumentException>());

        [Test]
        public void Process_ThrowsException_WordsIsNull() =>
            Assert.That(() => preprocessor.Process(null), Throws.InstanceOf<ArgumentException>());

        [Test]
        public void Process_ConvertWordsToLower()
        {
            var words = new List<string>() {"Привет", "МИР"};

            preprocessor.Process(words)
                .ToStrings()
                .Should().BeEquivalentTo("привет", "мир");
        }

        [TestCase("ах", TestName = "Interjection")]
        [TestCase("вот", TestName = "Particle")]
        [TestCase("из-за", TestName = "Pretext")]
        [TestCase("но", TestName = "Conjunction")]
        public void Process_Remove_BoringWords(string boringWord)
        {
            var words = new List<string> {"привет", "мир"};
            var prepositions = new List<string> {boringWord};

            preprocessor.Process(words.Concat(prepositions))
                .ToStrings()
                .Should().BeEquivalentTo(words);
        }
    }
}