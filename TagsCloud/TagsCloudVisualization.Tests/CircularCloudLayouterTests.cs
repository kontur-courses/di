using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloudVisualization.Tests.Extensions;

namespace TagsCloudVisualization.Tests
{
    public class CircularCloudLayouterTests
    {
        private readonly CircularCloudLayouterTestsLogger _logger = new();
        private Point _center;
        private CircularCloudLayouter _layouter;
        private Rectangle[] _rectangles;

        [SetUp]
        public void Setup()
        {
            _center = new Point(0, 0);
            _layouter = new CircularCloudLayouter(_center);
            _rectangles = Array.Empty<Rectangle>();
        }

        [OneTimeSetUp]
        public void Initialize()
        {
            _logger.Init(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestFails"));
        }

        [TearDown]
        public void TearDown()
        {
            var ctx = TestContext.CurrentContext;
            if (ctx.Result.Outcome.Status == TestStatus.Failed)
            {
                var testName = ctx.Test.Name;
                try
                {
                    _logger.Log(_rectangles, testName);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Cannot log test fail image due to exception: {e.Message}");
                }
            }
        }

        [TestCase(0, 1)]
        [TestCase(1, 0)]
        [TestCase(-1, 1)]
        [TestCase(1, -1)]
        public void PutNextRectangle_ShouldThrowException_WhenInvalidSize(int width, int height)
        {
            var size = new Size(width, height);
            Assert.Throws<ArgumentException>(() => _layouter.PutNextRectangle(size));
        }

        [Test]
        public void PutNextRectangle_ShouldBeCorrectSize()
        {
            var size = new Size(100, 100);

            var rectangle = _layouter.PutNextRectangle(size);
            _rectangles = new[] { rectangle };

            rectangle.Size.Should().Be(size);
        }

        [Test]
        public void PutNextRectangle_ShouldReturnNotIntersectedRectangles_WhenCalledTwice()
        {
            var size = new Size(100, 100);

            var firstRectangle = _layouter.PutNextRectangle(size);
            var secondRectangle = _layouter.PutNextRectangle(size);
            _rectangles = new[] { firstRectangle, secondRectangle };

            firstRectangle.IntersectsWith(secondRectangle).Should().BeFalse();
        }

        [Test]
        public void PutNextRectangle_ShouldReturnDifferentSizeRectangles_WhenCalledWithDifferentSizes()
        {
            var firstSize = new Size(100, 100);
            var secondSize = new Size(50, 100);

            var firstRectangle = _layouter.PutNextRectangle(firstSize);
            var secondRectangle = _layouter.PutNextRectangle(secondSize);
            _rectangles = new[] { firstRectangle, secondRectangle };

            firstRectangle.Size.Should().NotBe(secondRectangle.Size);
        }

        [TestCase(1, 10, 5)]
        public void PutNextRectangle_ShouldReturnRectangles_InCircle(
            int sideLength,
            int rectanglesCount,
            double boundingRadius)
        {
            var size = new Size(sideLength, sideLength);
            _rectangles = GenerateRectangles(rectanglesCount, () => size);

            _rectangles.Should().OnlyContain(rect => AreAllBoundsInCircle(rect, boundingRadius));
        }

        [TestCase(50, 0.7)]
        [TestCase(100, 0.75)]
        [TestCase(1000, 0.85, Ignore = "Take too much time")]
        public void PutNextRectangle_ShouldReturnRectangles_WithBigDensity_WhenSameSize(
            int rectanglesCount,
            double expectedRatio)
        {
            _rectangles = GenerateRectangles(rectanglesCount, () => new Size(10, 10));

            var rectanglesToCircleRatio = CalculateDensityForRectangles(_rectangles);

            rectanglesToCircleRatio.Should().BeGreaterOrEqualTo(expectedRatio);
        }

        [TestCase(50, 0.5)]
        [TestCase(100, 0.5)]
        [TestCase(1000, 0.5, Ignore = "Take too much time")]
        public void PutNextRectangle_ShouldReturnRectangles_WithBigDensity_WhenRandomSize(
            int rectanglesCount,
            double expectedRatio)
        {
            var rnd = new Random();
            _rectangles = GenerateRectangles(rectanglesCount, () => new Size(rnd.Next(1, 100), rnd.Next(1, 100)));

            var rectanglesToCircleRatio = CalculateDensityForRectangles(_rectangles);

            rectanglesToCircleRatio.Should().BeGreaterOrEqualTo(expectedRatio);
        }

        [TestCase(10, 1_000)]
        [TestCase(100, 10_000)]
        [TestCase(1_000, 30_000, Ignore = "Take too much time")]
        public void PutNextRectangle_ShouldGenerateRectanglesFast_WhenSameSize(int rectanglesCount, int maxMilliseconds)
        {
            var size = new Size(10, 10);
            _rectangles = new Rectangle[rectanglesCount];

            var stopWatch = Stopwatch.StartNew();
            for (var i = 0; i < rectanglesCount; i++) _rectangles[i] = _layouter.PutNextRectangle(size);
            stopWatch.Stop();

            stopWatch.Elapsed.Milliseconds.Should().BeLessOrEqualTo(maxMilliseconds);
        }

        private double CalculateDensityForRectangles(Rectangle[] rectangles)
        {
            var circleRadius = GetBoundingCircleRadius(rectangles);
            var rectanglesSquare =
                rectangles.Sum(rect => SquareCalculator.CalculateRectangleSquare(rect.Width, rect.Height));
            var circleSquare = SquareCalculator.CalculateCircleSquare(circleRadius);
            return rectanglesSquare / circleSquare;
        }

        private Rectangle[] GenerateRectangles(int count, Func<Size> sizeFactory)
        {
            return Enumerable.Range(0, count)
                .Select(_ => _layouter.PutNextRectangle(sizeFactory()))
                .ToArray();
        }

        private double GetBoundingCircleRadius(IEnumerable<Rectangle> rectangles)
        {
            var finder = new DistantPointFinder(_center);
            var rectanglesPoints = rectangles.SelectMany(GetBounds);
            return finder.GetDistantPoint(rectanglesPoints).DistanceTo(_center);
        }

        private bool AreAllBoundsInCircle(Rectangle rectangle, double radius)
        {
            return GetBounds(rectangle).All(bound => bound.DistanceTo(_center) <= radius);
        }

        private static IEnumerable<Point> GetBounds(Rectangle rectangle)
        {
            yield return new Point(rectangle.Left, rectangle.Top);
            yield return new Point(rectangle.Right, rectangle.Top);
            yield return new Point(rectangle.Right, rectangle.Bottom);
            yield return new Point(rectangle.Left, rectangle.Bottom);
        }
    }
}