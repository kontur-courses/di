using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.WordsPreprocessor;

namespace TagsCloudVisualization.Tests.WordsPreprocessor
{
    [TestFixture]
    public class RemoveBoredPreprocessorTests
    {
        [Test]
        public void Process_ShouldReturnLowerCaseWords()
        {
            var words = new[] { "word1", "word2", "a", "word3", "b" };
            var preprocessor = new RemoveBoredPreprocessor(new[] { "a", "b" });

            var processed = preprocessor.Process(words);

            processed.Should().ContainInOrder("word1", "word2", "word3");
        }
    }
}