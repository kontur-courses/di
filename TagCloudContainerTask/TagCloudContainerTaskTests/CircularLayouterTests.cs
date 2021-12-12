using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using App.Implementation.LayoutingAlgorithms.AlgorithmFromTDD;
using FluentAssertions;
using NUnit.Framework;

namespace TagCloudContainerTaskTests
{
    public class CircularLayouterTests
    {
        private CircularLayouter layouter;

        [SetUp]
        public void SetUp()
        {
            layouter = new CircularLayouter();
        }

        [TestCase(0, 0)]
        [TestCase(10, 10)]
        [TestCase(-1, -1)]
        public void Constructor_ShouldCreate_WithSpecifiedCenter(int x, int y)
        {
            var center = new Point(x, y);
            layouter = new CircularLayouter(center);

            layouter.Center.Should().BeEquivalentTo(center);
        }

        [Test]
        public void PutNextRectangle_ShouldReturnRectangle_AfterFirstPut()
        {
            var expectedRect = new Rectangle(Point.Empty, new Size(10, 10));

            var actualRect = layouter.PutNextRectangle(new Size(10, 10));

            actualRect.Size.Should().BeEquivalentTo(expectedRect.Size);
        }

        [TestCase(-1, -1)]
        [TestCase(-1, -0)]
        [TestCase(0, -1)]
        [TestCase(0, 0)]
        public void PutNextRectangle_ShouldThrow_WhenIncorrectSize(int width, int height)
        {
            var rectSize = new Size(width, height);
            Action act = () => layouter.PutNextRectangle(rectSize);

            act.Should().Throw<ArgumentException>("Incorrect size:", rectSize);
        }

        [Test]
        public void PutNextRectangle_ShouldAlignRectangleMiddlePointToCenter()
        {
            var rectSize = new Size(10, 10);

            var rect = layouter.PutNextRectangle(rectSize);

            rect.Location
                .Should()
                .BeEquivalentTo(new Point(-5, -5));
        }

        [TestCase(2)]
        [TestCase(4)]
        [TestCase(8)]
        [TestCase(16)]
        [TestCase(32)]
        [TestCase(64)]
        [TestCase(128)]
        [TestCase(256)]
        [Timeout(10000)]
        public void PutNextRectangle_ShouldNotIntersect_ForEachRectangle(int n)
        {
            var rectangles = PutNextNRectangles(n);

            IsIntersectExist(rectangles).Should().BeFalse();
        }

        [Test]
        public void CircularLayouter_ShouldPutRectanglesLikeCircle()
        {
            PutNextNRectangles(256);
            var expectedRatio = 0.8d;

            var actualRatio = GetDiametersRatio();

            actualRatio.Should().BeGreaterOrEqualTo(expectedRatio);
        }

        [Test]
        public void CircularLayouter_CloudDensityTest()
        {
            PutNextNRectangles(256);
            var expectedSquaresRatio = 0.5d;

            var actualSquaresRatio = GetSquaresRatio();

            actualSquaresRatio.Should().BeGreaterOrEqualTo(expectedSquaresRatio);
        }

        private double GetSquaresRatio()
        {
            var radius = layouter.GetCloudBoundaryRadius();
            var circleSquare = 2 * Math.PI * radius * radius;

            var boundaryBox = layouter.GetRectanglesBoundaryBox();
            var rectanglesSquare = boundaryBox.Width * boundaryBox.Height;

            return rectanglesSquare / circleSquare;
        }

        private double GetDiametersRatio()
        {
            var boundaryBox = layouter.GetRectanglesBoundaryBox();
            var biggestSide = (double)Math.Max(boundaryBox.Width, boundaryBox.Height);
            var smallestSide = (double)Math.Min(boundaryBox.Width, boundaryBox.Height);

            return smallestSide / biggestSide;
        }

        private List<Rectangle> PutNextNRectangles(int n)
        {
            return RectangleSizeGenerator.GetNextNRandomSizes(n)
                .Select(size => layouter.PutNextRectangle(size)).ToList();
        }

        private static bool IsIntersectExist(List<Rectangle> rectangles)
        {
            for (var i = 0; i < rectangles.Count; i++)
            {
                var rect = rectangles[i];
                for (var j = i + 1; j < rectangles.Count; j++)
                    if (rect.IntersectsWith(rectangles[j]))
                        return true;
            }

            return false;
        }
    }
}