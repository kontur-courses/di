using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Layouter;

namespace TagsCloudContainer.Tests
{
    public class TagCloudLayouter_Should
    {
        private Point center;
        private CircularCloudLayouter layouter;
        private readonly Size defaultSize = new Size(2, 4);


        [SetUp]
        public void SetUp()
        {
            var spiral = new Spiral(new SpiralSettings());
            center = spiral.GetCenter();
            layouter = new CircularCloudLayouter(spiral);
        }

        [Test]
        public void HaveCenter_AfterCreation()
        {
            layouter.Center.Should().Be(center);
        }

        [Test]
        public void NotChangeCenter_AfterPuttingNewRectangle()
        {
            layouter.PutNextRectangle(defaultSize);
            layouter.Center.Should().Be(center);
        }

        [Test]
        public void ReturnSameSizeRectangleAsGiven_WhenPuttingNewRectangle()
        {
            var rect = layouter.PutNextRectangle(defaultSize);
            rect.Size.Should().Be(defaultSize);
        }

        [Test]
        public void PutFirstRectangleCenterInCenter()
        {
            var rect = layouter.PutNextRectangle(defaultSize);
            rect.GetCenter().Should().Be(center);
        }

        [Test]
        public void NotHaveIntersectedRectangles()
        {
            var rects = AssignLayouter(Enumerable.Repeat(defaultSize, 10)).ToArray();

            var rectPairs = rects
                .Select((rectangle, i) => (rectangle: rectangle, possibleIntersectRectangles: rects.Skip(i + 1)));

            foreach (var rectPair in rectPairs)
                rectPair.rectangle.IntersectsWithAnyFrom(rectPair.possibleIntersectRectangles).Should().BeFalse();
        }

        [Test]
        public void PlaceFirstRectInARelativeCenter()
        {
            var rects = AssignLayouter(Enumerable.Repeat(defaultSize, 10)).ToArray();
            var middleX = (float)rects.Sum(rect => rect.X) / rects.Length;
            var middleY = (float)rects.Sum(rect => rect.Y) / rects.Length;
            var relativeCenter = new PointF(middleX, middleY);
            var distanceToFirst = rects.First().Location.DistanceTo(relativeCenter);
            foreach (var rectangle in rects.Skip(1))
                rectangle.Location.DistanceTo(relativeCenter).Should().BeGreaterOrEqualTo(distanceToFirst);
        }

        [TestCase(2, 8, 10000, TestName = "With High Rects")]
        [TestCase(15, 5, 1000, TestName = "With Long Rects")]
        [TestCase(10, 10, 1000, TestName = "With Squares")]
        public void BeTight(int width, int height, int repetitions)
        {
            var rects = AssignLayouter(Enumerable.Repeat(new Size(width, height), repetitions)).ToArray();
            var limitingCircleSquare = GetLimitingCircleSquare(rects);
            var rectsSumSquare = rects.Select(rect => rect.Width * rect.Height).Sum();
            (rectsSumSquare / limitingCircleSquare).Should().BeGreaterOrEqualTo(0.7);
        }

        //[TearDown]
        //public void DrawPictureIfFailed()
        //{
        //    if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed)
        //        return;
        //    var directory = TestContext.CurrentContext.TestDirectory;
        //    var bitmap = new Visualizer().Visualize(layouter.Rectangles.ToArray());
        //    var path = Path.Combine(directory, $"{TestContext.CurrentContext.Test.Name}.bmp");
        //    bitmap.Save(path);
        //    TestContext.Out.WriteLine($"Tag cloud visualization saved to file {directory}");
        //}

        private double GetLimitingCircleSquare(IEnumerable<Rectangle> rects)
        {
            var xDelta = rects.Select(rect => rect.Right).Max() - rects.Select(rect => rect.Left).Min();
            var yDelta = rects.Select(rect => rect.Bottom).Max() - rects.Select(rect => rect.Top).Min();
            var radius = Math.Min(xDelta, yDelta) / 2.0;
            return Math.PI * radius * radius;
        }

        private IEnumerable<Rectangle> AssignLayouter(IEnumerable<Size> sizes) =>
            sizes.Select(size => layouter.PutNextRectangle(size));
    }
}