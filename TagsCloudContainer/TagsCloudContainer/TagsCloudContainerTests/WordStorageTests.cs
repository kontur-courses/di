using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudContainer.TagsCloudContainerTests
{
    [TestFixture]
    class WordStorageTests
    {
        private WordStorage _wordStorage;

        [SetUp]
        public void SetUp()
        {
            _wordStorage = new WordStorage(new WordsCustomizer());
        }


        [TestCase("one", 1)]
        public void Simple_TMP_test0(string str, int count)
        {
            _wordStorage.Add(str);
            var lst = _wordStorage.ToList();
            lst[0].Value.Should().Be(str);
            lst[0].Count.Should().Be(count);
        }

        [TestCase]
        public void Simple_TMP_test1()
        {
            _wordStorage.AddRange(new List<string>() {"one", "Two", "two", "two2", "two2"});
            var lst = _wordStorage.ToList();
            lst[0].Value.Should().Be("two");
            lst[0].Count.Should().Be(2);

            lst[1].Value.Should().Be("two2");
            lst[1].Count.Should().Be(2);

            lst[2].Value.Should().Be("one");
            lst[2].Count.Should().Be(1);
        }
    }
}
