using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using Visualization;
using Visualization.Layouters.Spirals;

namespace CloudTagVisualizer.Tests
{
    [TestFixture]
    public class ExpandingSquare_Should
    {
        [TestCase(0, 0, TestName = "Center in (0, 0)")]
        [TestCase(5, 6, TestName = "Center is not in (0, 0)")]
        public void ReturnCenterPointOnFirstIteration_When(int x, int y)
        {
            var center = new Point(x, y);
            var square = new ExpandingSquareSpiral();

            var firstReturnerPoint = square.GetEnumerator(center).First();

            firstReturnerPoint.Should().BeEquivalentTo(center);
        }

        [Test]
        public void ReturnPointsInTopRightBottomLeftOrder()
        {
            var center = new Point(5, 6);
            var square = new ExpandingSquareSpiral();
            var expectedPoints = new List<Point>
            {
                center,
                center + new Size(-1, -1),
                center + new Size(0, -1),
                center + new Size(1, -1),
                center + new Size(1, 0),
                center + new Size(1, 1),
                center + new Size(0, 1),
                center + new Size(-1, 1),
                center + new Size(-1, 0),
            };

            var actualPoints = square
                .GetEnumerator(center)
                .Take(expectedPoints.Count)
                .ToList();

            actualPoints.Should().BeEquivalentTo(
                expectedPoints,
                config => config.WithStrictOrdering());
        }

        [Test]
        public void NotAffectOnOtherIntegerSquare()
        {
            var firstSquareCenter = new Point(10, 20);
            var firstSquare = new ExpandingSquareSpiral();

            var secondSquareCenter = new Point(-10, -20);
            var secondSquare = new ExpandingSquareSpiral();

            var takenPointsCount = 20;

            var firstSquarePoints = firstSquare
                .GetEnumerator(firstSquareCenter)
                .Take(takenPointsCount);

            var secondSquarePoints = secondSquare
                .GetEnumerator(secondSquareCenter)
                .Take(takenPointsCount);

            firstSquarePoints.Intersect(secondSquarePoints).Should().BeEmpty();
        }

        [Test]
        public void ReturnValuesWithIncreasingChebyshevDistance()
        {
            var center = new Point(5, 6);
            var square = new ExpandingSquareSpiral();

            var pointsCount = 500;
            var enumerator = square.GetEnumerator(center);
            using (new AssertionScope())
            {
                DistanceShouldExpire(enumerator.Take(pointsCount), center);
            }
        }

        [Test]
        [Timeout(1000)]
        public void ReturnPointsWithGoodTimePerformance()
        {
            var center = new Point(10, 20);
            var square = new ExpandingSquareSpiral();
            var takenPointsCount = 10000;

            square
                .GetEnumerator(center)
                .Take(takenPointsCount)
                .ToList();
        }

        private static void DistanceShouldExpire(IEnumerable<Point> points, Point startPoint)
        {
            var maxDistance = 0;
            foreach (var point in points)
            {
                var distance = Math.Max(
                    Math.Abs(point.X - startPoint.X),
                    Math.Abs(point.Y - startPoint.Y));
                distance.Should().BeGreaterOrEqualTo(maxDistance);

                maxDistance = distance;
            }
        }
    }
}