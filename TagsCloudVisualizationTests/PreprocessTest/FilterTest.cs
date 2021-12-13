using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.WordsPreprocessors.Filters;

namespace TagsCloudVisualizationTests.Preprocess
{
    public class FilterTest
    {
        [Test]
        public void Filter_ShouldFilteredBoringWords()
        {
            var words = new[] { "text", "a", "b", "text2" };
            var filter =  new BoringWordsFilter(new[] { "a", "b" });

            var processed = filter.Filter(words);

            processed.Should().ContainInOrder("text", "text2");
        }
    }
}