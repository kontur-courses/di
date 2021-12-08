using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudContainer.TextPreparation
{
    public class DefaultWordHelperTests
    {
        private DefaultWordHelper helper;

        [SetUp]
        public void InitWordHelper()
        {
            helper = new DefaultWordHelper();
        }

        [Test]
        public void GetAllWordsToVisualize_Throws_WhenWordsIsNull()
        {
            Action act = () => helper.GetAllWordsToVisualize(null);

            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetAllWordsToVisualize_RemovesAllShortWords()
        {
            helper.GetAllWordsToVisualize(new List<string>() {"a", "bb", "ccc"}).Should().BeEmpty();
        }

        [Test]
        public void GetAllWordsToVisualize_ConvertAllWordsToLowerCase()
        {
            var expectedResult = new List<string>() {"aaaa", "bbbb"};

            helper.GetAllWordsToVisualize(new List<string>() {"AAAA", "bBBb"}).Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void GetAllWordsToVisualize_NotAddsRecurringWords()
        {
            var expectedResult = new List<string>() {"aaaa"};

            helper.GetAllWordsToVisualize(new List<string>() {"aaaa", "aaaa"}).Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void GetAllWordsToVisualize_NotAddsRecurringWordsInDifferentCases()
        {
            var expectedResult = new List<string>() {"aaaa"};

            helper.GetAllWordsToVisualize(new List<string>() {"aaaa", "aAaa"}).Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void GetAllWordsToVisualize_SortWordsByPopularity()
        {
            var expectedResult = new List<string>() {"aaaa", "bbbb", "cccc"};

            helper.GetAllWordsToVisualize(new List<string>()
                {
                    "aaaa",
                    "aaaa",
                    "bbbb",
                    "cccc",
                    "bbbb",
                    "aaaa"
                })
                .Should()
                .BeEquivalentTo(expectedResult);
        }

        [Test]
        public void GetAllWordsToVisualize_SortWordsByOrderOfAppearance_WhenPopularityIsTheSame()
        {
            var expectedResult = new List<string>() {"aaaa", "bbbb"};

            helper.GetAllWordsToVisualize(new List<string>() {"aaaa", "bbbb", "aaaa", "bbbb"})
                .Should()
                .BeEquivalentTo(expectedResult);
        }
    }
}