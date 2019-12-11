using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudGenerator.WordsHandler;
using TagsCloudGenerator.WordsHandler.Converters;
using TagsCloudGenerator.WordsHandler.Filters;

namespace TagsCloudGeneratorTests.WordsHandler
{
    public class WordHandlerTests
    {
        private readonly List<string> boringWords = new List<string>
            {"in", "with", "are", "have", "coherent applying converters "};

        private WordHandler handlerWithOnlyOneConverter;
        private WordHandler handlerWithOnlyOneFilter;


        private class TakeTenElementsFilter : IWordsFilter
        {
            public Dictionary<string, int> Filter(Dictionary<string, int> wordToCount)
            {
                var validWords = new Dictionary<string, int>();
                var i = 0;

                foreach (var word in wordToCount.TakeWhile(word => i < 10))
                {
                    validWords.Add(word.Key, word.Value);
                    i++;
                }

                return validWords;
            }
        }

        private class SpaceAdder : IConverter
        {
            public Dictionary<string, int> Convert(Dictionary<string, int> wordToCount)
            {
                return wordToCount.ToDictionary(word => word.Key + " ", word => word.Value);
            }
        }

        [SetUp]
        public void SetUp()
        {
            var filter = new TakeTenElementsFilter();
            var converter = new SpaceAdder();
            var realFilter = new BoringWordsEjector();
            realFilter.AddBoringWords(boringWords);
            var realConverter = new LowercaseConverter();

            handlerWithOnlyOneFilter = new WordHandler(new List<IWordsFilter>{realFilter}, new List<IConverter>{converter, realConverter});
            handlerWithOnlyOneConverter = new WordHandler(new List<IWordsFilter>{filter, realFilter}, new List<IConverter>{realConverter});
        }

        [Test]
        public void GetValidWords__ShouldReturnComplexResultAllFiltersAndConverter()
        {
            var data = new Dictionary<string, int>
            {
                ["fish"] = 2,
                ["suN"] = 1,
                ["cat"] = 40,
                ["have"] = 33,
                ["In"] = 1,
            };
            var expected = new Dictionary<string, int>
            {
                ["fish"] = 2,
                ["sun"] = 1,
                ["cat"] = 40,
            };

            var actual = handlerWithOnlyOneConverter.GetValidWords(data);

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetValidWords_OnlyInvalidWordsWithUppercaseLetters_ShouldReturnEmptyDictionary()
        {
            var data = new Dictionary<string, int>
            {
                ["In"] = 2,
                ["haVe"] = 33
            };

            var actual = handlerWithOnlyOneConverter.GetValidWords(data);

            actual.Should().BeEmpty();
        }

        [Test]
        public void GetValidWords_WordsMoreThanTakeFilter_ShouldReturnValidCountWords()
        {
            var data = new Dictionary<string, int>
            {
                ["fish"] = 2,
                ["suN"] = 1,
                ["cat"] = 40,
                ["water"] = 33,
                ["earth"] = 1,
                ["angel"] = 4,
                ["beer"] = 10,
                ["chEEse"] = 2,
                ["cloud"] = 5,
                ["math"] = 44,
                ["photo"] = 1,
                ["line"] = 2
            };
            var expected = new Dictionary<string, int>
            {
                ["fish"] = 2,
                ["sun"] = 1,
                ["cat"] = 40,
                ["water"] = 33,
                ["earth"] = 1,
                ["angel"] = 4,
                ["beer"] = 10,
                ["cheese"] = 2,
                ["cloud"] = 5,
                ["math"] = 44
            };

            var actual = handlerWithOnlyOneConverter.GetValidWords(data);

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetValidWords_ShouldReturnResultCoherentApplyingConvertersAndFilter()
        {
            var data = new Dictionary<string, int>
            {
                ["angel"] = 4,
                ["BEER"] = 10,
                ["Cheese"] = 2,
                ["cloud"] = 5,
                ["maTh"] = 44,
                ["coherent applying converters"] = 5,
                ["suN"] = 1,
                ["cat"] = 40,
                ["water"] = 33,
            };
            var expected = new Dictionary<string, int>
            {
                ["angel "] = 4,
                ["beer "] = 10,
                ["cheese "] = 2,
                ["cloud "] = 5,
                ["math "] = 44,
                ["sun "] = 1,
                ["cat "] = 40,
                ["water "] = 33,
            };

            var actual = handlerWithOnlyOneFilter.GetValidWords(data);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}