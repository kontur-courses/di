using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudForm.WordFilters;

namespace TagsCloudTests.FiltersTests
{
    public class PartOfSpeechFilterTests
    {
        [Test]
        public void PartOfSpeechFilter_FilterTest_ShouldFilterNotWords()
        {
            var checker = new PartOfSpeechFilter();
            var words = new string[] { "www", "hello", "asd", "to" };
            var partOfSpeechToFilter = new HashSet<string> { "TO" };

            var filtered = checker.Filter(partOfSpeechToFilter, words);

            filtered.Should().BeEquivalentTo(new string[] { "www", "hello", "asd" });
        }

        [Test]
        public void PartOfSpeechFilter_NothingInToFilter_ShouldNotChangeInput()
        {
            var checker = new PartOfSpeechFilter();
            var words = new string[] { "www", "hello", "asd", "to" };
            var partOfSpeechToFilter = new HashSet<string>();

            var filtered = checker.Filter(partOfSpeechToFilter, words);

            filtered.Should().BeEquivalentTo(words);
        }
    }
}
