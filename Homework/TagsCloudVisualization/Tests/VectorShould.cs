using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    internal class VectorShould
    {
        [Test]
        public void InitializeFieldsAfterInstanceCreation()
        {
            var begin = new Point(0, 0);
            var end = new Point(1, 1);

            var vector = new Vector(begin, end);

            vector.Begin.Should().BeEquivalentTo(begin);
            vector.End.Should().BeEquivalentTo(end);
        }

        [TestCaseSource(nameof(GetVectorLengthTestData))]
        public void CalculateCorrectLength(Point begin, Point end, double expectedLength)
        {
            var vectorLength = new Vector(begin, end).GetLength();

            vectorLength.Should().Be(expectedLength);
        }

        private static IEnumerable<TestCaseData> GetVectorLengthTestData()
        {
            yield return new TestCaseData(new Point(0, 0), new Point(0,0), 0)
                .SetName("when the beginning and the end are the same");
            yield return new TestCaseData(new Point(0, 0), new Point(-3, -4), 5)
                .SetName("when coordinates are negative");
            yield return new TestCaseData(new Point(1, 1), new Point(4, 5), 5)
                .SetName("when common case is given");
        }
    }
}
