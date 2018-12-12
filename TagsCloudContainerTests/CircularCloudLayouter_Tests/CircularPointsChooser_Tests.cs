using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using TagsCloudContainer.CircularCloudLayouters;
using TagsCloudContainer.Extensions;
using TagsCloudContainer.Settings;

namespace TagsCloudContainerTests.CircularCloudLayouter_Tests
{
    [TestFixture]
    public class CircularPointsChooser_Tests
    {
        private IEnumerator<Point> pointsChooser;
        private Point centerPoint;

        [SetUp]
        public void SetUp()
        {
            var imageSettings = new ImageSettings();
            var size = imageSettings.ImageSize;
            centerPoint = new Point(size.Width / 2, size.Height / 2);
            pointsChooser = new CircularPointsChooser(new ImageSettings());
        }

        [Test]
        public void Current_ReturnsCenterPoint_WhenUsedMoveNextOneTime()
        {
            pointsChooser.MoveNext();
            pointsChooser.Current.ShouldBeEquivalentTo(centerPoint);
        }

        [Test]
        public void Current_ReturnsDifferentPoints_AfterEachMoveNext()
        {
            var points = GetPointsOrder(10000);

            points.Should().OnlyHaveUniqueItems();
        }

        [Test]
        public void Current_ReturnsPoints_InAscendingDistanceOrder()
        {
            var points = GetPointsOrder(10000);
            var distances = GetDistancesToCenter(points);
            distances.Should().BeInAscendingOrder(new DoubleHandler());
        }

        [Test]
        public void Current_ReturnsPoints_WithNotTooBigAverageDistance()
        {
            var countItems = 10000;
            var points = GetPointsOrder(countItems);
            var distances = GetDistancesToCenter(points);
            var averageDistance = distances.Sum() / countItems;

            averageDistance.Should().BeLessOrEqualTo(500);
        }

        private List<Point> GetPointsOrder(int count)
        {
            var points = new List<Point>();
            for (var index = 0; index < count; index++)
            {
                pointsChooser.MoveNext();
                points.Add(pointsChooser.Current);
            }

            return points;
        }

        private IEnumerable<double> GetDistancesToCenter(IEnumerable<Point> points)
        {
            return points.Select(x => x.DistanceTo(centerPoint)).ToList();
        }

        private class DoubleHandler : IComparer<double>
        {
            private const double DeltaDistance = 1;

            public int Compare(double x, double y)
            {
                var difference = Math.Abs(x - y);
                return difference <= DeltaDistance ? 0 : x.CompareTo(y);
            }
        }

    }
}