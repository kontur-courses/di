using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudForm.WordFilters;

namespace TagsCloudTests.FiltersTests
{
    [TestFixture]
    public class BoringWordsTests
    {
        [Test]
        public void BoringWordsFilter_NoBoringWords_ShouldNotChangeInput()
        {
            var filter = new BoringWordsFilter();
            var words = new List<string> { "www", "hello", "asd" };
            var boringWords = new HashSet<string>();

            var filtered = filter.Filter(boringWords, words);

            filtered.Should().BeEquivalentTo(words);
        }


        [Test]
        public void BoringWordsFilter_FilterTest_ShouldFilterGivenWords()
        {
            var filter = new BoringWordsFilter();
            var words = new List<string> { "www", "hello", "asd" };
            var boringWords = new HashSet<string> { "asd", "www"};

            var filtered = filter.Filter(boringWords, words);

            filtered.Should().BeEquivalentTo(new List<string> {"hello"});
        }
    }
}
