using System;
using System.Collections.Generic;
using System.Drawing;
using CloudLayouters;
using FluentAssertions;
using NUnit.Framework;

namespace CircularCloudTests
{
    [TestFixture]
    public class ArchimedeanSpiral_Should
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
            var set = new HashSet<Point>();
            for (var i = 0; i < 10000; i++) set.Add(spiral.GetNextPoint());
            set.Count.Should().Be(10000);
        }

        [Test]
        public void GetNextPoint_Void_PointsLocatedCloseEnoughToEachOther()
        {
            var previousPoint = spiral.GetNextPoint();
            for (var i = 0; i < 10000; i++)
            {
                var currentPoint = spiral.GetNextPoint();
                var distance = Math.Sqrt((previousPoint.X - currentPoint.X) * (previousPoint.X - currentPoint.X) +
                                         (previousPoint.Y - currentPoint.Y) * (previousPoint.Y - currentPoint.Y));
                distance.Should().BeLessOrEqualTo(5,
                    "distance between points should be less or equal to 5 pixels. " +
                    $"Failed with Point number {i + 1} = {previousPoint} , and point number {i + 2} = {currentPoint}");
                previousPoint = currentPoint;
            }
        }
    }
}