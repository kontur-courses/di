using System.Collections.Generic;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.repositories;
using TagCloud.selectors;

namespace TagCloudTests
{
    [TestFixture]
    public class WordHelperTests
    {
        private List<string> words;
        private IChecker<string> checker;
        private IFilter<string> filter;
        private IConverter<string> singleConverter;
        private IConverter<IEnumerable<string>> converter;

        [SetUp]
        public void SetUp()
        {
            words = new List<string>
            {
                "word",
                "WORD",
            };

            checker = A.Fake<IChecker<string>>();
            filter = new WordFilter(new List<IChecker<string>> { checker });
            singleConverter = A.Fake<IConverter<string>>();
            converter = new WordConverter(new List<IConverter<string>> { singleConverter });
        }

        [Test]
        public void FilterWords_ShouldNotIgnoreOnlyValidWords()
        {
            A.CallTo(() => checker.IsValid("word")).Returns(true);
            A.CallTo(() => checker.IsValid("WORD")).Returns(false);
            var helper = new WordHelper(filter, null);
            helper.FilterWords(words)
                .Should()
                .NotContain("WORD")
                .And.Contain("word");
        }

        [Test]
        public void GetWordStatistics_ShouldReturnCorrectStatistics()
        {
            var helper = new WordHelper(null, null);
            var result = new List<WordStatistic> { new("word", 1), new("WORD", 1) };
            helper.GetWordStatistics(words)
                .Should()
                .BeEquivalentTo(result);
        }

        [Test]
        public void ConvertWords_ShouldConvertAllWord()
        {
            A.CallTo(() => singleConverter.Convert("word")).Returns("_word_");
            A.CallTo(() => singleConverter.Convert("WORD")).Returns("_WORD_");
            var helper = new WordHelper(null, converter);
            var result = new List<string> { "_word_", "_WORD_" };
            helper.ConvertWords(words)
                .Should()
                .BeEquivalentTo(result);
        }
    }
}