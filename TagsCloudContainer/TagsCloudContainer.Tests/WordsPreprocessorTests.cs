using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

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
        public void Process_ThrowsException_WordsIsNull() =>
            Assert.That(() => preprocessor.Process(null), Throws.InstanceOf<ArgumentException>());

        [Test]
        public void Process_Delete_DuplicatedWords()
        {
            var words = new List<string> {"Мир", "МИР"};
            preprocessor.Process(words)
                .ToWords()
                .Should().BeEquivalentTo("мир");
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
                .ToWords()
                .Should().BeEquivalentTo(words);
        }
    }
}