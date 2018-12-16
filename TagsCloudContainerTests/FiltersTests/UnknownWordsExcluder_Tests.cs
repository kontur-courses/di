using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Dictionaries;
using TagsCloudContainer.WordsFilters;

namespace TagsCloudContainerTests.FiltersTests
{
    [TestFixture]
    public class UnknownWordsExcluder_Tests
    {
        private IGrammarDictionary grammarDictionary;
        private readonly IReadOnlyList<string> correctWords = new List<string>{"word", "another", "an"};
        private UnknownWordsExcluder unknownWordsExcluder;

        [SetUp]
        public void SetUp()
        {
            grammarDictionary = A.Fake<IGrammarDictionary>();
            A.CallTo(() => grammarDictionary.ContainsWord(A<string>.Ignored)).Returns(false);
            AddToDictionaryCorrectWords(grammarDictionary, correctWords);
            unknownWordsExcluder = new UnknownWordsExcluder(grammarDictionary);
        }

        [Test]
        public void IsCorrectWord_ReturnsTrue_OnCorrectWord()
        {
            unknownWordsExcluder.IsCorrect(correctWords.First()).Should().BeTrue();
        }

        [Test]
        public void IsCorrectWords_ReturnFalse_OnUnknownWord()
        {
            unknownWordsExcluder.IsCorrect("incorrectWord").Should().BeFalse();
        }


        private void AddToDictionaryCorrectWords(IGrammarDictionary grammarDictionary, IEnumerable<string> words)
        {
            foreach (var word in words)
            {
                A.CallTo(() => grammarDictionary.ContainsWord(word)).Returns(true);
            }
        }
    }
}