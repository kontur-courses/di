using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.WordsPreprocessor;

namespace TagsCloudVisualization.Tests.WordsPreprocessor
{
    [TestFixture]
    public class ToLowerCasePreprocessorTests
    {
        private ToLowerCasePreprocessor _preprocessor;

        [SetUp]
        public void Setup()
        {
            _preprocessor = new ToLowerCasePreprocessor();
        }

        [Test]
        public void Process_ShouldReturnLowerCaseWords()
        {
            var words = new[] { "First", "SECOND" };
            var expected = new[] { "first", "second" };

            var processed = _preprocessor.Process(words);

            processed.Should().ContainInOrder(expected);
        }
    }
}