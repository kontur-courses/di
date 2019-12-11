using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudGenerator.WordsHandler.Filters;

namespace TagsCloudGeneratorTests.WordsHandler.FiltersTests
{
    public class BoringWordsEjectorTests
    {
        private readonly List<string> boringWords = new List<string> {"in", "with", "are", "have"};
        private BoringWordsEjector filter;

        [SetUp]
        public void SetUp()
        {
           filter =  new BoringWordsEjector();
           filter.AddBoringWords(boringWords);
        }

        [Test]
        public void Filter_AllWordsIsValid_ShouldReturnThisWords()
        {
            var words = new Dictionary<string, int>
            {
                ["fish"] = 2,
                ["sun"] = 1,
                ["cat"] = 40,
                ["sofa"] = 1
            };

            var actual = filter.Filter(words);

            actual.Should().BeEquivalentTo(words);
        }

        [Test]
        public void Filter_ArgumentIsNull_ShouldThrowArgumentNullException()
        {
            Dictionary<string, int> words = null;

            Action act = () => filter.Filter(words);

            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Filter_AllWordsIsInvalid_ShouldReturnEmptyDictionary()
        {
            var words = new Dictionary<string, int>
            {
                ["have"] = 2,
                ["in"] = 1,
                ["with"] = 40,
                ["are"] = 1
            };

            var actual = filter.Filter(words);

            actual.Should().BeEmpty();
        }

        [Test]
        public void Filter_FewWordsIsInvalid_ShouldReturnOnlyValidWords()
        {
            var words = new Dictionary<string, int>
            {
                ["sun"] = 2,
                ["in"] = 1,
                ["black"] = 40,
                ["wrong"] = 15,
                ["are"] = 3
            };
            var expected = new Dictionary<string, int>
            {
                ["sun"] = 2,
                ["black"] = 40,
                ["wrong"] = 15,
            };

            var actual = filter.Filter(words);

            actual.Should().BeEquivalentTo(expected);
        }

    }
}