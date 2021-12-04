using System;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudVisualization.Tests
{
    public class DistantPointFinderTests
    {
        [Test]
        public void GetDistantPoint_ShouldReturnSamePoint_WhenSinglePoint()
        {
            var finder = new DistantPointFinder(new Point(0, 0));
            var points = new[] { new Point(10, 10) };

            var distantPoint = finder.GetDistantPoint(points);

            distantPoint.Should().Be(points.Single());
        }

        [Test]
        public void GetDistantPoint_ShouldReturnCorrectPoint_WhenSeveralPoints()
        {
            var finder = new DistantPointFinder(new Point(0, 0));
            var points = new[] { new Point(10, 10), new Point(0, 0), new Point(100, 100) };

            var distantPoint = finder.GetDistantPoint(points);

            distantPoint.Should().Be(points.Last());
        }

        [Test]
        public void GetDistantPoint_ShouldThrowException_WhenPointsIsEmpty()
        {
            var finder = new DistantPointFinder(new Point(0, 0));
            var points = Array.Empty<Point>();

            Assert.Throws<InvalidOperationException>(() => finder.GetDistantPoint(points));
        }
    }
}