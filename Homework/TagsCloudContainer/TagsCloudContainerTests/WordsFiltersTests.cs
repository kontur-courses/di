using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.WordsFilter;

namespace TagsCloudContainerTests
{
    [TestFixture]
    public class WordsFilters_Should
    {
        [Test]
        public void RemoveWord()
        {
            var data = new Dictionary<string, int>
            {
                {"a", 1 },
                {"aa", 2 },
                {"aaa", 1 }
            };
            var OnDeleteWord = "A";

            var expected = new Dictionary<string, int>
            {
                {"aa", 2 },
                {"aaa", 1}
            };

            var actually = data.RemoveWord(OnDeleteWord);

            actually.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void RemoveWordsForList()
        {
            var data = new Dictionary<string, int>
            {
                {"a", 1 },
                {"aa", 2 },
                {"aaa", 1 }
            };

            var OnDeleteWords = new List<string>
            {
                "A",
                "aA"
            };
            var expected = new Dictionary<string, int>
            {
                {"aaa", 1}
            };

            var actually = data.RemoveWords(OnDeleteWords);

            actually.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void RemoveWordsLessThanSpecificLength()
        {
            var data = new Dictionary<string, int>
            {
                {"a", 1 },
                {"aa", 1 },
                {"aaa", 1 }
            };

            var leftBound = 2;

            var expected = new Dictionary<string, int>
            {
                {"aa", 1 },
                {"aaa", 1}
            };

            var actually = data.RemoveWordsOutOfLengthRange(leftBound);

            actually.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void RemoveWordsOutOfSpecificLengthRange()
        {
            var data = new Dictionary<string, int>
            {
                {"a", 1 },
                {"aa", 1 },
                {"aaa", 1 }
            };

            var leftBound = 2;
            var rightBound = 2;

            var expected = new Dictionary<string, int>
            {
                {"aa", 1 }
            };

            var actually = data.RemoveWordsOutOfLengthRange(leftBound, rightBound);

            actually.Should().BeEquivalentTo(expected);
        }
    }
}
