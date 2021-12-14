using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.WordsPreprocessors;
using TagsCloudVisualization.WordsPreprocessors.Filters;
using TagsCloudVisualization.WordsPreprocessors.Preparers;


namespace TagsCloudVisualizationTests.PreprocessTest
{
    public class PreprocessTest
    {
        [Test]
        public void Preprocess_Should()
        {
            var words = new[] { "TEXT", "A", "B", "TEXT2" };
            var preprocessor = new WordsPreprocessor(
                new[] { new WordsToLowerPrepare() }, new[] { new BoringWordsFilter(new[] { "a", "b" }) });

            var processed = preprocessor.Preprocess(words);

            processed
                .Should()
                .BeEquivalentTo(new[] { "text", "text2" }, options => 
                    options.WithStrictOrdering());
        }
    }
}