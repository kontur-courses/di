using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Logic;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class PointExtensionsTests
    {
        [TestCase(0, 0, 0, TestName = "Zero point has zero length")]
        [TestCase(2,2, 2.8284, TestName = "x and y are positive")]
        [TestCase(-3, -4, 5, TestName = "x and y are negative")]
        [TestCase(0, 5, 5, TestName = "one of values is zero")]
        public void GetLength_ReturnsCorrectResult_When(int x, int y, double expectedLength)
        {
            var epsilon = 0.0001;
            var vector = new Point(x, y);
            vector.GetLength().Should().BeApproximately(expectedLength, epsilon);
        }
    }
}