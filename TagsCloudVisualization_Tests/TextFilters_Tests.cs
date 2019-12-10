using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.TextFilters;

namespace TagsCloudVisualization_Tests
{
    public class TextFilters_Tests
    {
        private List<string> boringWords;
        private ITextFilter boringWordsFilter;
        private ITextFilter repeatingWordsFilter;
        private ITextFilter shortWordsFilter;

        [SetUp]
        public void SetUp()
        {
            boringWords = new List<string> {"ну", "а", "э"};
            boringWordsFilter = new BoringWordsFilter(boringWords);
            repeatingWordsFilter = new RepeatingWordsFilter();
            shortWordsFilter = new ShortWordsFilter(3);
        }

        [Test]
        public void BoringWordsFilter_WithUsersBoringWords_ShouldReturnRightResult()
        {
            var words = new List<string>
                {"ну", "слова", "попадут", "в", "результат", "э", "не", "попадут", "в", "фильтр"};
            var expectedWords = new List<string> {"слова", "попадут", "в", "результат", "не", "попадут", "в", "фильтр"};
            boringWordsFilter.FilterWords(words).Should().BeEquivalentTo(expectedWords);
        }

        [Test]
        public void BoringWordsFilter_WithUsersBoringWordsAndAllWordsBoring_ShouldEmptyCollection()
        {
            var words = new List<string> {"ну", "э", "э", "а", "а", "э"};
            boringWordsFilter.FilterWords(words).Should().BeEmpty();
        }

        [Test]
        public void RepeatingWordsFilter_WithoutRepeatingWords_ShouldReturnUnchangedWords()
        {
            var words = new List<string> {"а", "б", "в", "г", "д", "е"};
            repeatingWordsFilter.FilterWords(words).Should().BeEquivalentTo(words);
        }

        [Test]
        public void RepeatingWordsFilter_WithRepeatingWords_ShouldReturnUniqueWords()
        {
            var words = new List<string> {"а", "б", "в", "а", "а", "е", "е"};
            var expectedResult = new List<string> {"а", "б", "в", "е"};
            repeatingWordsFilter.FilterWords(words).Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void ShortWordsFilter_WithShortWords_ShouldReturnLongWords()
        {
            var words = new List<string> {"а", "боль", "время", "а", "он", "они", "стоп"};
            var expectedResult = new List<string> {"боль", "время", "стоп"};
            shortWordsFilter.FilterWords(words).Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void ShortWordsFilter_WithoutShortWords_ShouldReturnUnchangedWords()
        {
            var words = new List<string> {"слова", "длинные", "попадут", "результат"};
            shortWordsFilter.FilterWords(words).Should().BeEquivalentTo(words);
        }
    }
}