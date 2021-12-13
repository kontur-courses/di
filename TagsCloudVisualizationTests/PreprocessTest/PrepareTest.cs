using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.WordsPreprocessors.Preparers;

namespace TagsCloudVisualizationTests.Preprocess
{
    public class PrepareTest
    {
        [Test]
        public void Prepare_ShouldReturnLowerCase()
        {
            var words = new[] { "TEXT", "A", "B", "TEXT2" };
            var preparer = new WordsToLowerPrepare();

            var processed = preparer.Prepare(words);

            processed.Should().ContainInOrder("text", "a", "b", "text2");
        }
    }
}