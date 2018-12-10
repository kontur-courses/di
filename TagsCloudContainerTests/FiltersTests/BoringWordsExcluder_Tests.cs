using System.Collections.Generic;
using NUnit.Framework;
using FakeItEasy;
using TagsCloudContainer.BoringWordsGetters;
using TagsCloudContainer.WordsFilters;
using FluentAssertions;

namespace TagsCloudContainerTests.FiltersTests
{
    [TestFixture]
    public class BoringWordsExcluder_Tests
    {
        private IBoringWordsGetter boringWordsGetter;
        private IFilter<string> filter;
        private readonly string[] baseWords = { "a", "an", "aQuiteLongWord" };

        [SetUp]
        public void SetUp()
        {
            boringWordsGetter = GetBoringWordsGetter(baseWords);
            filter = new BoringWordsExcluder(new[] { boringWordsGetter });
        }

        [TestCase("A", TestName = "given word is uppercase")]
        [TestCase("a", TestName = "word are equals to one of base words")]
        [TestCase("aN", TestName = "given word is not full uppercase")]
        public void IsCorrectWord_ReturnsFalse_WhenBoringWordsGetterContainGivenWord(string word)
        {
            filter.IsCorrect(word).Should().BeFalse();
        }

        [TestCase("aQuiteLongWor", TestName = "given string is a substring of base word")]
        [TestCase("aQuiteLongWordA", TestName = "base word is a substring of base word")]
        [TestCase("aaaaaaaaa", TestName = "base words do not contain given word")]
        public void IsCorrectWord_ReturnsTrue_WhenBoringWordsGetterDoesNotContainGivenWord(string word)
        {
            filter.IsCorrect(word).Should().BeTrue();
        }

        [Test]
        public void IsCorrectWord_ReturnsTrue_WhenThereAreNoBoringWords()
        {
            SetBoringWords(new string[] { });
            filter.IsCorrect("a").Should().BeTrue();
        }

        [Test]
        public void IsCorrectWord_ReturnsFalse_WhenSecondBoringWordsGetterContainsGivenWord()
        {
            var newBoringWordsGetter = GetBoringWordsGetter(new[] { "another", "words" });
            var filterBoringWords = new BoringWordsExcluder(new [] {boringWordsGetter, newBoringWordsGetter});

            filterBoringWords.IsCorrect("another").Should().BeFalse();
        }

        private void SetBoringWords(IEnumerable<string> boringWords)
        {
            A.CallTo(() => boringWordsGetter.GetBoringWords()).Returns(boringWords);
            filter = new BoringWordsExcluder(new[] { boringWordsGetter });
        }

        private IBoringWordsGetter GetBoringWordsGetter(IEnumerable<string> boringWords)
        {
            var boringWordsGetter = new Fake<IBoringWordsGetter>().FakedObject;
            A.CallTo(() => boringWordsGetter.GetBoringWords()).Returns(boringWords);
            return boringWordsGetter;
        }
    }
}