using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudContainer.TagsCloudContainerTests
{
    [TestFixture]
    internal class WordStorageShould
    {
        private WordStorage _wordStorage;

        [SetUp]
        public void SetUp()
        {
            _wordStorage = new WordStorage(new WordsCustomizer(), new List<string>());
        }

        [TestCase("one", 1)]
        public void AddWordCorrectly(string str, int count)
        {
            _wordStorage.Add(str);
            var result = _wordStorage.GetOrderedByWordsFrequency().ToList();
            result[0].Value.Should().Be(str);
            result[0].Count.Should().Be(count);
        }

        [Test]
        public void AddAllElementsFromRange()
        {
            _wordStorage.AddRange(new List<string> {"one", "two", "three", "four", "five"});
            _wordStorage.GetOrderedByWordsFrequency().ToList().Count.Should().Be(5);
        }

        [Test]
        public void AddEmptyRangeCorrectly()
        {
            _wordStorage.AddRange(new List<string>());
            _wordStorage.GetOrderedByWordsFrequency().ToList().Count.Should().Be(0);
        }

        [Test]
        public void IgnoreLetterCase()
        {
            _wordStorage.AddRange(new List<string> { "Two", "two"});
            var result = _wordStorage.GetOrderedByWordsFrequency().ToList();
            result[0].Value.Should().Be("two");
            result[0].Count.Should().Be(2);
        }

        [Test]
        public void SaveIncomingWordsOrderWhenEqualCounts()
        {
            _wordStorage.AddRange(new List<string> {"two", "two", "two2", "two2"});
            var result = _wordStorage.GetOrderedByWordsFrequency().ToList();
            result[0].Value.Should().Be("two");

            result[1].Value.Should().Be("two2");
        }

        [Test]
        public void SortWordsByItsAmount()
        {
            _wordStorage.AddRange(new List<string> { "four", "four", "four", "four", "two", "two", "one", "three", "three", "three" });
            var result = _wordStorage.GetOrderedByWordsFrequency().ToList();

            for (var i = 0; i < result.Count; i++)
                result[i].Count.Should().Be(4 - i);
        }
    }
}