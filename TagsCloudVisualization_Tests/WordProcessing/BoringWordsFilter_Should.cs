using System;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using TagsCloudVisualization.WordsProcessing;

namespace TagsCloudVisualization_Tests.WordProcessing
{
    [TestFixture]
    public class BoringWordsFilter_Should
    {
        private IWordsProvider wordsProvider;
        private BoringWordsFilter filter;

        [SetUp]
        public void SetUp()
        {
            wordsProvider = Substitute.For<IWordsProvider>();
            wordsProvider.Provide().Returns(new[] {"a", "b"});
            filter = new BoringWordsFilter(wordsProvider);
        }

        [Test]
        public void FilterWordsCorrectly_WhenBoringWords()
        {
            var words = new [] {"a", "b", "c"};
            var expected = new [] {"c" };
            filter.FilterWords(words).Should().BeEquivalentTo(expected);
        }

        [Test]
        public void FilterWordsCorrectly_WhenNoBoringWords()
        {
            var words = new[] { "n", "x", "c" };
            var expected = new[] { "n", "x", "c" };
            filter.FilterWords(words).Should().BeEquivalentTo(expected);
        }

        [Test]
        public void FilterWordsCorrectly_WhenOnlyBoringWords()
        {
            var words = new[] { "a", "b" };
            var expected = Array.Empty<string>();
            filter.FilterWords(words).Should().BeEquivalentTo(expected);
        }
    }
}
