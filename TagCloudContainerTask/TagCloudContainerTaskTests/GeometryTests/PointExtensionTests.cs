using System;
using System.Collections.Generic;
using System.Drawing;
using App.Implementation.GeometryUtils;
using FluentAssertions;
using NUnit.Framework;

namespace TagCloudContainerTaskTests.GeometryTests
{
    public class PointExtensionTests
    {
        [TestCase(1, 1)]
        [TestCase(1, -1)]
        [TestCase(-1, 1)]
        [TestCase(-1, -1)]
        public void MovePoint_ShouldMovePoint(int x, int y)
        {
            var originPoint = Point.Empty;

            var movedPoint = originPoint.MovePoint(x, y);

            movedPoint.Should().BeEquivalentTo(new Point(originPoint.X + x, originPoint.Y + y));
        }

        [Test]
        public void GetDistanceTo_ShouldCalculateDistanceTo(
            [ValueSource(nameof(PointsFrom))] Point from,
            [ValueSource(nameof(PointsTo))] Point to)
        {
            var expectedDistance = GetDistanceFromTo(from, to);

            var actualDistance = from.GetDistanceTo(to);


            actualDistance.Should().Be(expectedDistance);
        }

        private double GetDistanceFromTo(Point from, Point to)
        {
            return Math.Sqrt((to.X - from.X) * (to.X - from.X)
                             + (to.Y - from.Y) * (to.Y - from.Y));
        }

        private static IEnumerable<Point> PointsFrom()
        {
            yield return Point.Empty;

            yield return new Point(40, 25);
            yield return new Point(-45, -5);
            yield return new Point(-8, 42);
        }

        private static IEnumerable<Point> PointsTo()
        {
            yield return Point.Empty;

            yield return new Point(22, -44);
            yield return new Point(35, 50);
            yield return new Point(6, 18);
        }
    }
}