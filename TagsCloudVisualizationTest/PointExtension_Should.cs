using System.Drawing;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudVisualizationTest
{
    [TestFixture]
    public class PointExtension_Should
    {
        [TestCase(0, 0, 0, 0, 0)]
        [TestCase(1, 0, 0, 0, 1)]
        [TestCase(-1, 0, 0, 0, 1)]
        [TestCase(0, 1, 0, 0, 1)]
        [TestCase(0, -1, 0, 0, 1)]
        [TestCase(0, 3, 4, 0, 5)]
        [TestCase(-3, 0, 0, -4, 5)]
        [TestCase(2, 5, 6, 2, 5)]
        public void PointExtension_ShouldCorrect(int x1, int y1, int x2, int y2, double expected)
        {
            var firstPoint = new Point(x1, y1);
            var secondPoint = new Point(x2, y2);
            var actual = firstPoint.MetricTo(secondPoint);

            actual.Should().Be(expected, $"Point 1: x={x1}, y={y1}; Point 2: x={x2}, y={y2}; expected={expected}");
        }
    }
}