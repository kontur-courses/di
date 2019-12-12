using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.Styling.Themes;
using TagsCloudVisualization.Visualizers;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class SpiralLayouterConstructor_Should
    {
        [Test]
        public void ThrowArgumentException_WhenSpiralIsNull()
        {
            Action action = () => new SpiralLayouter(null);

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void CreateNewInstance_WhenCenterCoordinatesArePositive()
        {
            var center = new PointF(150, 250);

            Action action = () => new SpiralLayouter(new Spiral(center));

            action.Should().NotThrow<Exception>();
        }
    }
    
    [TestFixture]
    public class SpiralLayouterPutNextRectangle_Should
    {
        private PointF layouterCenter;
        private SpiralLayouter layouter;
        private List<RectangleF> layouterRectangles;

        [SetUp]
        public void Init()
        {
            layouterCenter = new PointF(500, 500);
            layouter = new SpiralLayouter(new Spiral(layouterCenter));
            layouterRectangles = new List<RectangleF>();
        }

        [TearDown]
        public void TearDown()
        {
            var testContext = TestContext.CurrentContext;
            if (testContext.Result.Outcome.Status == TestStatus.Failed)
            {
                var directory = AppDomain.CurrentDomain.BaseDirectory;
                var testName = testContext.Test.Name;
                var time = DateTime.Now.ToString("yy-MM-dd hh-mm-ss");
                var filename = Path.Combine(directory, $"{testName} {time}.png");
                new TextNoRectanglesVisualizer()
                    .Visualize(new GrayDarkTheme(), layouterRectangles)
                    .Save(filename, ImageFormat.Png);
                Console.WriteLine($"Tag cloud visualization saved to file {filename}");
            }
        }

        [TestCase(0, 10, TestName = "Width x is zero")]
        [TestCase(10, 0, TestName = "Height y is zero")]
        [TestCase(-1, 10, TestName = "Width x is negative")]
        [TestCase(10, -1, TestName = "Height y is negative")]
        public void ThrowArgumentException_When(int width, int height)
        {
            Action action = () => layouter.PutNextRectangle(new SizeF(width, height));

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void AddFirstRectangleInTheCloudCenter()
        {
            var addedRectangle = layouter.PutNextRectangle(new SizeF(100, 200));

            layouterRectangles = new List<RectangleF> {addedRectangle};

            addedRectangle.Location.Should().BeEquivalentTo(layouterCenter);
        }

        [Test]
        public void AddMultipleRectangles_That_DontIntersectWithEachOther()
        {
            var rectangles = new List<RectangleF>
            {
                layouter.PutNextRectangle(new SizeF(100, 200)),
                layouter.PutNextRectangle(new SizeF(130, 250)),
                layouter.PutNextRectangle(new SizeF(210, 160)),
                layouter.PutNextRectangle(new SizeF(120, 115))
            };

            rectangles.Any(r1 => rectangles.Any(r2 => r1.IntersectsWith(r2) && r1 != r2)).Should().BeFalse();
        }

        [Test]
        public void AddNextRectangle_That_DoesntIntersectWithFirst()
        {
            var firstRectangle = layouter.PutNextRectangle(new SizeF(100, 200));
            var secondRectangle = layouter.PutNextRectangle(new SizeF(50, 100));

            layouterRectangles = new List<RectangleF> {firstRectangle, secondRectangle};

            secondRectangle.IntersectsWith(firstRectangle).Should().BeFalse();
        }

        [Test]
        public void NotChangeRectangleSize()
        {
            var addedRectangle = layouter.PutNextRectangle(new SizeF(100, 200));

            layouterRectangles = new List<RectangleF> {addedRectangle};

            addedRectangle.Size.Should().BeEquivalentTo(new SizeF(100, 200));
        }

        [Test]
        public void PlaceTwoRectanglesCloseToEachOther()
        {
            var acceptableYAxisShift = 5;
            var acceptableXAxisShift = 25;
            var firstRectangle = layouter.PutNextRectangle(new SizeF(100, 100));
            var secondRectangle = layouter.PutNextRectangle(new SizeF(20, 102));

            layouterRectangles = new List<RectangleF> {firstRectangle, secondRectangle};

            secondRectangle.Y.Should().BeInRange(firstRectangle.Top - acceptableYAxisShift,
                firstRectangle.Top + acceptableYAxisShift);
            secondRectangle.X.Should().BeInRange(firstRectangle.Left - acceptableXAxisShift,
                firstRectangle.Right + acceptableXAxisShift);
        }

        [TestCase(50, 40, 40, 2048, TestName = "Rectangles are squares")]
        [TestCase(50, 50, 70, 1000, TestName = "Rectangles are big (50 to 70)")]
        [TestCase(50, 30, 50, 42, TestName = "Rectangles are medium (30 to 50)")]
        [TestCase(50, 15, 30, 777, TestName = "Rectangles are small (15 to 30)")]
        [TestCase(1000, 70, 100, 555555, TestName = "Rectangles count is big (1000)")]
        [TestCase(10, 10, 20, -555, TestName = "Rectangles count is small (10)")]
        public void AddMultipleRectangles_That_FormACircleLikeShape_When(int count, int minSize, int maxSize,
            int randomSeed)
        {
            var acceptableRatio = 50;
            var random = new Random(randomSeed);

            layouterRectangles =
                RectangleGenerator.GenerateRandomRectangles(layouter, count, minSize, maxSize, random);

            var furthestDistance = layouterRectangles
                .Select(r => GetDistanceBetweenRectangleAndPoint(r, layouterCenter))
                .Max();
            var rectanglesSquare = layouterRectangles
                .Aggregate(0d, (current, rectangle) => current + rectangle.Width * rectangle.Height);
            var circleSquare = furthestDistance * furthestDistance * Math.PI;
            var squareRatio = rectanglesSquare / circleSquare * 100;
            squareRatio.Should().BeGreaterOrEqualTo(acceptableRatio);
        }

        private static double GetDistanceBetweenRectangleAndPoint(RectangleF rectangle, PointF point)
        {
            var rectangleCentre = new PointF(rectangle.Location.X + rectangle.Width / 2,
                rectangle.Location.Y + rectangle.Height / 2);

            return Math.Sqrt(Math.Pow(rectangleCentre.X - point.X, 2) + Math.Pow(rectangleCentre.Y - point.Y, 2));
        }
    }
}