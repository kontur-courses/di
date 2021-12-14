using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.WordsPreprocessors.Filters;

namespace TagsCloudVisualizationTests.PreprocessTest
{
    public class FilterTest
    {
        [Test]
        public void Filter_ShouldFilteredBoringWords()
        {
            var words = new[] { "text", "a", "b", "text2" };
            var filter =  new BoringWordsFilter(new[] { "a", "b" });

            var processed = filter.Filter(words);

            processed
                .Should()
                .BeEquivalentTo(new[] { "text", "text2" }, options => 
                    options.WithStrictOrdering());
        }
        
        [Test]
        public void Filter_ShouldFilterOtherCaseBoringWords()
        {
            var words = new[] { "text", "a", "b", "text2" };
            var filter =  new BoringWordsFilter(new[] { "A", "B" });

            var processed = filter.Filter(words);

            processed
                .Should()
                .BeEquivalentTo(new[] { "text", "text2" }, options => 
                    options.WithStrictOrdering());
        }
    }
}