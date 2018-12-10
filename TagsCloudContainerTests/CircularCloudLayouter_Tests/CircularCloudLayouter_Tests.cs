using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NUnit.Framework;
using TagsCloudContainer.WordsFilters;
using FakeItEasy;
using FluentAssertions;
using TagsCloudContainer.CircularCloudLayouters;
using TagsCloudContainerTests.Extensions;

namespace TagsCloudContainerTests.CircularCloudLayouter_Tests
{
    [TestFixture]
    public class CircularCloudLayouter_Tests
    {
        private IFilter<Rectangle> filterPoints;
        private readonly List<Point> basePoints = new List<Point>();
        private IEnumerator<Point> pointsOrder;
        private CircularCloudLayouter circularCloudLayouter;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            for (var index = 100; index < 10000; index++)
                basePoints.Add(new Point(index, index));
        }

        [SetUp]
        public void SetUp()
        {
            filterPoints = new Fake<IFilter<Rectangle>>().FakedObject;
            A.CallTo(() => filterPoints.IsCorrect(A<Rectangle>.Ignored)).Returns(true);
            SetBaseLayouterWithCurrentPoints(basePoints);
        }

        [Test]
        public void PutNextRectangle_ReturnsRectangle_WhichContainsStartPoint()
        {
            var size = new Size(20, 20);

            var nextRectangle = circularCloudLayouter.PutNextRectangle(size);

            nextRectangle.Contains(basePoints.First()).Should().BeTrue();
        }

        [Test]
        public void PutNextRectangle_ReturnsNonIntersectingRectangle_WhenThereAreFewRectangles()
        {
            var sizes = new[] { new Size(20, 20), new Size(30, 30) };

            var rectangles = circularCloudLayouter.PutNextRectangles(sizes);

            AssertThatRectanglesDoNotIntersect(rectangles);
        }

        [Test]
        public void PutNextRectangle_ReturnsThirdRectangle_WhichDoesNotIntersectsWithFirst_WhenThereAreThreeSizes()
        {
            var sizes = new[] { new Size(20, 20), new Size(30, 30), new Size(20, 20) };
            var points = new[] { new Point(0, 0), new Point(100, 100), new Point(10, 10), new Point(50, 50) };
            SetBaseLayouterWithCurrentPoints(points);

            var rectangles = circularCloudLayouter.PutNextRectangles(sizes);

            rectangles[0].IntersectsWith(rectangles[2]).Should().BeFalse();
        }

        [Test, Timeout(1000)]
        public void PutNextRectangle_WorksFast_WhenThereAreManySizes()
        {
            var sizes = Enumerable.Range(0, 500).Select(x => new Size(10, 10)).ToArray();

            var rectangles = circularCloudLayouter.PutNextRectangles(sizes);
        }

        [Test]
        public void PutNextRectangle_DoNotReturnsRectangles_WhichDoNotSatisfyFilter()
        {
            var size = new Size(10, 10);
            var points = new[] {new Point(10, 10), new Point(100, 100)};
            SetBaseLayouterWithCurrentPoints(points);
            A.CallTo(() => filterPoints.IsCorrect(A<Rectangle>.Ignored)).Returns(false).Once();

            var rectangle = circularCloudLayouter.PutNextRectangle(size);

            rectangle.Contains(points.First()).Should().BeFalse();
        }

        private void AssertThatRectanglesDoNotIntersect(List<Rectangle> rectangles)
        {
            rectangles.SelectMany(x => rectangles.Select(y => Tuple.Create(x, y)))
                .Where(x => x.Item1 != x.Item2)
                .Select(x => x.Item1.IntersectsWith(x.Item2))
                .ShouldAllBeEquivalentTo(false);
        }

        private void SetBaseLayouterWithCurrentPoints(IEnumerable<Point> points)
        {
            pointsOrder = points.GetEnumerator();
            circularCloudLayouter = new CircularCloudLayouter(pointsOrder, filterPoints);
        }
    }
}