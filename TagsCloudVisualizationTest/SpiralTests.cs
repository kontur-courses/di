using NUnit.Framework;
using System.Drawing;
using FluentAssertions;
using System.Collections.Generic;
using TagsCloudVisualization.PointDistributors;

namespace TagsCloudVisualizationTest
{
    [TestFixture]
    public class SpiralTests
    {
        [Test]
        [TestOf(nameof(Spiral.GetPosition))]
        public void WhenPassFirstPoint_ShouldBeInCenter()
        {
            var spiral = new Spiral(new Point(0, 0), 5, 1.5);

            var newPosition = spiral.GetPosition();

            newPosition.Should().Be(new Point(0, 0));
        }

        public static TestCaseData[] ArgumentsForSpiralTests =
        {
            new TestCaseData(new Point(0, 0), new List<Point>
            {
                new Point(0, 0),
                new Point(0, 1),
                new Point(-2, 0),
                new Point(0, -3),
                new Point(4, -1),
                new Point(2, 5),
                new Point(-6, 2),
                new Point(-3, -7)
            }).SetName("WhenGetFewPointsFromCenter_ShouldReturnExpectedPoint"),

            new TestCaseData(new Point(-3, 2), new List<Point>() //Arrange
            {
                new Point(-3, 2),
                new Point(-3, 3),
                new Point(-5, 2),
                new Point(-3, -1),
                new Point(1, 1),
                new Point(-1, 7),
                new Point(-9, 4),
                new Point(-6, -5)
            }).SetName("WhenGetFewPointsFromOffsetCenter_ShouldReturnExpectedPoint")
        };

        [TestOf(nameof(Spiral.GetPosition))]
        [TestCaseSource(nameof(ArgumentsForSpiralTests))]
        public void WhenGetFewPoints_ShouldDrawSpiral(Point center, List<Point> expectedPoints)
        {
            var spiral = new Spiral(center, 5, 1.5);
            var actualPositions = new Point[8];

            for (var i = 0; i < expectedPoints.Count; i++)
                actualPositions[i] = spiral.GetPosition();

            for (var i = 0; i < expectedPoints.Count; i++)
                actualPositions[i].Should().Be(expectedPoints[i]);
        }
    }
}
