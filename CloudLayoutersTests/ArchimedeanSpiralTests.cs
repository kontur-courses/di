using System;
using System.Drawing;
using CloudLayouters;
using FluentAssertions;
using NUnit.Framework;

namespace CircularCloudTests
{
    [TestFixture]
    public class ArchimedeanSpiralTests
    {
        [SetUp]
        public void SetUp()
        {
            spiral = new ArchimedeanSpiral(Point.Empty);
        }

        private ArchimedeanSpiral spiral;

        [Test]
        public void GetNextPoint_Void_FirstPointIsCenter()
        {
            spiral.GetNextPoint().Should().Be(spiral.Center);
        }

        [Test]
        public void GetNextPoint_Void_ReturnsDifferentPoints()
        {
            var next = spiral.GetNextPoint();
            for (var i = 0; i < 10000; i++)
            {
                var current = next;
                next = spiral.GetNextPoint();
                Math.Abs(GetDistanceBetwenPoints(current, spiral.Center) - GetDistanceBetwenPoints(next, spiral.Center))
                    .Should()
                    .BeLessOrEqualTo(1);
            }
        }

        [Test]
        public void GetNextPoint_Void_PointsLocatedCloseEnoughToEachOther()
        {
            var previousPoint = spiral.GetNextPoint();
            for (var i = 0; i < 10000; i++)
            {
                var currentPoint = spiral.GetNextPoint();
                var distance = GetDistanceBetwenPoints(previousPoint, currentPoint);
                distance.Should().BeLessOrEqualTo(5,
                    "distance between points should be less or equal to 5 pixels. " +
                    $"Failed with Point number {i + 1} = {previousPoint} , and point number {i + 2} = {currentPoint}");
                previousPoint = currentPoint;
            }
        }

        private static double GetDistanceBetwenPoints(Point first, Point second)
        {
            return Math.Sqrt((first.X - second.X) * (first.X - second.X) +
                             (first.Y - second.Y) * (first.Y - second.Y));
        }
    }
}