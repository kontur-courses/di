using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagCloud;

namespace TagCloudTest
{
    [TestFixture]
    public class SpiralTests
    {
        private Spiral spiral;

        [SetUp]
        public void SetUp()
        {
            spiral = new Spiral(new Point(0, 0));
        }

        [Test]
        public void SpiralStartsAtCenter()
        {
            var startingPoint = new Point(1, 5);
            var shiftedSpiral = new Spiral(startingPoint);

            shiftedSpiral.CurrentPoint.Should().Be(startingPoint);
        }

        [Test]
        public void DistanceBetween2PointsAfterFullSpin_ShouldIncrease()
        {
            spiral.Next();
            var point1 = spiral.CurrentPoint;

            for (var i = 0; i < 360 / spiral.AngleStep; i++)
                spiral.Next();
            var point2 = spiral.CurrentPoint;

            for (var i = 0; i < 360 / spiral.AngleStep; i++)
                spiral.Next();
            var point3 = spiral.CurrentPoint;
            int DistSquared(Point p1, Point p2) => (p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y);

            DistSquared(point1, point2).Should().BeGreaterThan(DistSquared(point2, point3));
        }
    }
}