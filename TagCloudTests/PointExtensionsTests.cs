using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Extensions;

namespace TagCloudTests
{
    public class PointExtensionsTests
    {
        [TestCase(0, 0, 1, 2, TestName = "any point and non-zero direction")]
        [TestCase(0, 0, 1, 0, TestName = "any point and non-zero X direction")]
        [TestCase(3, 5, 0, 5, TestName = "any point and non-zero Y direction")]
        public void PointShiftTo_ReturnedMovedPoint_WhenSet(int pointX, int pointY, int shiftX, int shiftY)
        {
            var point = new Point(pointX, pointY);
            var movementDirection = new Size(shiftX, shiftY);

            var movedPoint = Point.Add(point, movementDirection);

            PointShiftTo_CheckShift(point, movementDirection, movedPoint);
        }

        [Test]
        public void PointShiftTo_ReturnedNotMovedPoint_WhenSetZeroDirection()
        {
            var point = new Point(5, 6);

            PointShiftTo_CheckShift(point, new Size(0,0), point);
        }

        public void PointShiftTo_CheckShift(Point point, Size movementDirection, Point movedPoint)
        {
            var shiftedPoint = point.ShiftTo(movementDirection);

            shiftedPoint.Should().BeEquivalentTo(movedPoint);
        }
    }
}
