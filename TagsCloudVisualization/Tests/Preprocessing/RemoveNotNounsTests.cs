using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Preprocessing;

namespace TagsCloudVisualization.Tests.Preprocessing
{
    [TestFixture]
    public class RemoveNotNounsTests
    {
        [Test]
        public void RemoveNotNouns_WorksCorrectly() //Test may not work because it requires loading module file.
        {
            var words = new[] {"apple", "gold", "brain", "meat", "chicken", "golden", "wonderful", "runner", "blow"};
            var expectedResult = new[] {"apple", "gold", "brain", "meat", "chicken", "runner"};
            var preprocessor = new RemoveNotNounsPreprocessor();
            var actualResult = preprocessor.ProcessWords(words).ToArray();
            actualResult.Should().BeEquivalentTo(expectedResult);
        }
    }
}