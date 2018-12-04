using System;
using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudContainer.CircularCloudLayouter.CircularCloudLayouterTests
{
    [TestFixture]
    public class SectorTests
    {
        [SetUp]
        public void SetUp()
        {
            _points = new List<Point>();
        }

        [TearDown]
        public void TearDown()
        {
            _points = null;
        }

        private List<Point> _points;


        [TestCase(10, 5, TestName = "ShouldFindThreePoint")]
        public void FindPointsToInsert_WhenPointsEmpty(int maxX, int maxY)
        {
            _points.Add(new Point(0, 0));
            var rangeToRemove = new Tuple<int, int>(0, 0);

            var result = Sector.FindPointsToInsert(
                new Size(maxX, maxY), _points, rangeToRemove, maxX, maxY);
            var expected = new List<Point>
            {
                new Point(0, maxY),
                new Point(maxX, maxY),
                new Point(maxX, 0)
            };

            result.Should().BeEquivalentTo(expected);
        }

        [TestCase(9, 7, 2, 4, new[] {4, 7, 9, 7, 9, 3}, 5, 2,
            TestName = "WhenPlaceWithWidthRightBorder")]
        [TestCase(10, 7, 2, 4, new[] {4, 7, 10, 7, 10, 3}, 6, 2,
            TestName = "WhenPlaceWithWidherRightBorder")]
        [TestCase(10, 11, 0, 4, new[] {0, 11, 10, 11, 10, 3}, 6, 6,
            TestName = "WhenHighestAndAlmostWidthest")]
        [TestCase(8, 9, 0, 2, new[] {0, 9, 8, 9, 8, 5}, 4, 4,
            TestName = "WhenPlaceWithHightLeftBorder")]
        [TestCase(8, 10, 0, 2, new[] {0, 10, 8, 10, 8, 5}, 4, 5,
            TestName = "WhenPlaceWithHighestLeftBorder")]
        [TestCase(100, 100, 0, 7, new[] {0, 100, 100, 100, 100, 0}, 10, 10,
            TestName = "WhenAllPointsSmaller")]
        [TestCase(11, 9, 0, 7, new[] {0, 9, 11, 9, 11, 0}, 10, 10,
            TestName = "WhenBordersEqual")]
        [TestCase(8, 7, 2, 2, new[] {4, 7, 8, 7, 8, 5}, 4, 2,
            TestName = "WhenPlaceInMiddle")]
        public void FindPointsToInsert_ShouldFindThreePoints(
            int maxX, int maxY, int left, int right, int[] resultPointValues, int width, int height)
        {
            _points.AddRange(new List<Point>
            {
                new Point(0, 9),
                new Point(4, 9),
                new Point(4, 5),
                new Point(9, 5),
                new Point(9, 3),
                new Point(11, 3),
                new Point(11, 0)
            });

            var rangeToRemove = new Tuple<int, int>(left, right);

            var result = Sector.FindPointsToInsert(
                new Size(width, height), _points, rangeToRemove, maxX, maxY);
            var expected = new List<Point>
            {
                new Point(resultPointValues[0], resultPointValues[1]),
                new Point(resultPointValues[2], resultPointValues[3]),
                new Point(resultPointValues[4], resultPointValues[5])
            };

            result.Should().BeEquivalentTo(expected);
        }
    }
}