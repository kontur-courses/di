using System;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Logic;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class GeometryTests
    {
        [TestCase(10, 0, 10, 0, TestName = "Length is positive, angle is 0")]
        [TestCase(15, 2.0 / 3, 11, 9, TestName = "Length is positive, angle is 2/3")]
        [TestCase(0, 1, 0, 0, TestName = "Length is 0")]
        [TestCase(-10, 2, 4, -9, TestName = "Length is negative number")]
        [TestCase(40, -8, -5, -39, TestName = "Angle is negative number")]
        public void PolarToCartesian_ReturnsCorrectPoint(double ro, double phi, int x, int y)
        {
            Geometry.PolarToCartesianCoordinates(ro, phi).Should().Be(new Point(x, y));
        }


        [TestCase(-1, 1, TestName = "Width is negative number")]
        [TestCase(1, -1, TestName = "Height is negative number")]
        [TestCase(int.MinValue, int.MinValue, TestName = "Both height and width are negative numbers")]
        public void ShiftPointBySizeOffsets_ThrowsArgumentException(int width, int height)
        {
            Action action = () => Geometry.ShiftPointBySizeOffsets(Point.Empty, new Size(width, height));

            action.Should().Throw<ArgumentException>();
        }

        [TestCase(0, 0, 10, 10, TestName = "Center is zero")]
        [TestCase(0, 0, 0, 10, TestName = "Center is zero, width is zero")]
        [TestCase(0, 0, 10, 0, TestName = "Center is zero, height is zero")]
        [TestCase(25, -10, 10, 10, TestName = "Center is non-zero")]
        public void ShiftPointBySizeOffsets_ReturnsCorrectPoint(int x, int y, int width, int height)
        {
            var shiftedPoint = Geometry.ShiftPointBySizeOffsets(new Point(x, y), new Size(width, height));

            shiftedPoint.Should().Be(new Point(x - width / 2, y - height / 2));
        }

        [TestCase(0, 0, 10, 10, 15, 0, 5, TestName = "Center is zero, angle between (center,end) and OX is 0")]
        [TestCase(0, 0, 10, 10, 15, 15, 7.071, TestName = "Center is zero, angle between (center,end) and OX is 45")]
        [TestCase(0, 0, 20, 20, 15, 30, 11.180, TestName = "Center is zero, angle between (center,end) and OX is ~63")]
        [TestCase(9, 3, 2, 2, 8, 1, 1.118, TestName = "Center is non-zero, angle between (center,end) and OX is ~63")]
        public void GetLengthFromRectCenterToBorderOnVector_ReturnsCorrectLength(
            int centerX, int centerY, int width, int height, int endX, int endY, double expectedLength)
        {
            var epsilon = 0.001;
            var rectangle = new Rectangle(centerX, centerY, width, height);
            var endPoint = new Point(endX, endY);

            var length = Geometry.GetLengthFromRectangleCenterToBorderOnVector(rectangle, endPoint);

            length.Should().BeInRange(expectedLength - epsilon, expectedLength + epsilon);
        }

        [TestCase(-1, 1, TestName = "Width is negative number")]
        [TestCase(1, -1, TestName = "Height is negative number")]
        [TestCase(int.MinValue, int.MinValue, TestName = "Both height and width are negative numbers")]
        public void GetLengthFromRectangleCenterToBorderOnVector_ThrowsArgumentException(int width, int height)
        {
            Action action = () =>
                Geometry.GetLengthFromRectangleCenterToBorderOnVector(new Rectangle(0, 0, width, height), Point.Empty);

            action.Should().Throw<ArgumentException>();
        }

        [TestCase(0, 0, 10, 10, 0, 0, TestName = "Center is zero and point is zero")]
        [TestCase(0, 0, 10, 10, 3, -3, TestName = "Center is zero and non-zero point inside")]
        [TestCase(10, -40, 20, 20, 5, -35, TestName = "Center is non-zero and point inside")]
        [TestCase(0,0,10,10,0,5, TestName  = "Point is on rectangle border")]
        public void GetLengthFromRectCenterToBorderOnVector_ReturnsZero(
            int centerX, int centerY, int width, int height, int endX, int endY)
        {
            var rectangle = new Rectangle(centerX, centerY, width, height);
            var endPoint = new Point(endX, endY);

            var length = Geometry.GetLengthFromRectangleCenterToBorderOnVector(rectangle, endPoint);

            length.Should().Be(0);
        }
    }
}