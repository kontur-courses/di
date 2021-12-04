using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.WordsPreprocessor;

namespace TagsCloudVisualization.Tests.WordsPreprocessor
{
    [TestFixture]
    public class CombinedPreprocessorTests
    {
        [Test]
        public void Process_ShouldReturnLowerCaseWords()
        {
            var words = new[] { "WORD1", "WORD2", "B", "WORD3", "A" };
            var preprocessor = new CombinedPreprocessor(
                new ToLowerCasePreprocessor(),
                new RemoveBoredPreprocessor(new[] { "a", "b" })
            );

            var processed = preprocessor.Process(words);

            processed.Should().ContainInOrder("word1", "word2", "word3");
        }
    }
}