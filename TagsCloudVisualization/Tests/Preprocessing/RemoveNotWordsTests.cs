using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Preprocessing;

namespace TagsCloudVisualization.Tests.Preprocessing
{
    [TestFixture]
    public class RemoveNotWordsTests
    {
        [Test]
        public void RemoveNotWords_WorksCorrectly()
        {
            var words = new[]
            {
                "asdf", "123-51", "hell'es", "SMILE", "RIGHT", "NOW", "GOOD", "TESTS", "Ding-dong", "  ", "--\\asdzc12"
            };
            var expectedResult = new[] {"asdf", "hell'es", "SMILE", "RIGHT", "NOW", "GOOD", "TESTS", "Ding-dong"};
            var preprocessor = new RemoveNotWordsPreprocessor();
            var actualResult = preprocessor.ProcessWords(words).ToArray();
            actualResult.Should().BeEquivalentTo(expectedResult);
        }
    }
}