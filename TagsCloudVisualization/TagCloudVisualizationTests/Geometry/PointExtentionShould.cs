using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Geometry;

namespace TagCloudVisualizationTests.Geometry
{
    [TestFixture]
    class PointExtensionTest
    {
        [Test]
        public void ReturnZero_When_TheSamePoints()
        {
            var point = new Point(1, 3);
            point.DistanceTo(point).Should().Be(0);
        }

        [TestCase(1, 0, 1, 0, 0, TestName = "Return_Zero_When_PointsIsEquals")]
        [TestCase(6, 0, 0, 8, 10, TestName = "Return_RightDistance_OnDifferentPoints")]
        public void DistanceTo_Should(int x1, int y1, int x2, int y2, double distance)
        {
            new Point(x1, y1).DistanceTo(new Point(x2, y2)).Should().Be(distance);
        }
    }
}
