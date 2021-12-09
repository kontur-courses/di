using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Extensions;
using Moq;
using NUnit.Framework;
using TagsCloudVisualization;
using TagsCloudVisualizationTests.TestingLibrary;

namespace TagsCloudVisualizationTests.Tests
{
    public class CircularCloudLayouterTests
    {
        private ICloudLayouter layouter;
        private List<Rectangle> resultingRectangles;

        [SetUp]
        public void SetUp()
        {
            layouter = CreateMockLayouter(new CircularCloudLayouter());
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.FailCount == 0 || resultingRectangles.Count == 0)
                return;

            var output = new VisualOutput(new RectangleVisualizer(resultingRectangles));
            var savePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "CircularCloudLayouter.TestFail.bmp");

            output.SaveToBitmap(savePath);
            TestContext.WriteLine($"Tag cloud visualization saved to file {savePath}");
        }

        [Test]
        public void Constructor_ThrowsException_WithNullSpiralPath()
        {
            Assert.Throws<ArgumentException>(
                () =>
                    new CircularCloudLayouter(new Point(), null));
        }

        [TestCaseSource(nameof(PutNextRectangleFirstRectanglePlacedInCenterTestCases))]
        public Point PutNextRectangle_FirstRectangle_PlacedInCenter(Point center, Size rectangle)
        {
            layouter = CreateMockLayouter(new CircularCloudLayouter(center));
            var actual = layouter.PutNextRectangle(rectangle);
            return actual.Location;
        }

        public static IEnumerable<TestCaseData> PutNextRectangleFirstRectanglePlacedInCenterTestCases()
        {
            var size = new Size(3, 4);
            yield return new TestCaseData(new Point(0, 0), size) {ExpectedResult = new Point(-1, -2)};
            yield return new TestCaseData(new Point(3, 4), size) {ExpectedResult = new Point(2, 2)};
            yield return new TestCaseData(new Point(-3, -4), size) {ExpectedResult = new Point(-4, -6)};
        }

        [Test]
        public void PutNextRectangle_SecondRectangle_NotIntersectFirst()
        {
            var first = layouter.PutNextRectangle(new Size(1, 1));
            var second = layouter.PutNextRectangle(new Size(10, 10));
            second.IntersectsWith(first).Should().BeFalse();
        }

        [Test]
        public void PutNextRectangle_RandomRectangles_NotIntersect()
        {
            var rectangles = new List<Rectangle>();
            LayouterBitmapSaver.CreateRandomRectangles(100).ForEach(
                rectangle =>
                {
                    TestContext.WriteLine(rectangle);
                    rectangles.Add(layouter.PutNextRectangle(rectangle));
                    AssertHaveNoIntersection(rectangles);
                });
        }

        [TestCase(0, 0)]
        [TestCase(1, 0)]
        [TestCase(0, 1)]
        [TestCase(-1, -1)]
        [TestCase(-1, 1)]
        [TestCase(1, -1)]
        public void PutNextRectangle_ThrowsException_WithNotPositiveSize(int width, int height)
        {
            Assert.Throws<ArgumentException>(
                () =>
                    layouter.PutNextRectangle(new Size(width, height)));
        }

        [Test]
        public void PutNextRectangle_WorksFastEnough()
        {
            var rectangles = LayouterBitmapSaver.CreateRandomRectangles(1000);

            Action act = () => rectangles.ForEach(rectangle => layouter.PutNextRectangle(rectangle));

            GC.Collect();
            act.ExecutionTime().Should().BeLessThan(5.Seconds());
        }

        [Test]
        public void PutNextRectangle_RandomRectangles_DensityHighEnough()
        {
            var rectangles = LayouterBitmapSaver
                .CreateRandomRectangles(1000)
                .Select(layouter.PutNextRectangle)
                .ToList();

            var density = CalculateDensity(rectangles);
            density.Should().BeGreaterThan(0.65);
            TestContext.WriteLine($"Density is: {density}");
        }

        private static double CalculateDensity(IReadOnlyCollection<Rectangle> rectangles)
        {
            var topLeft = PointHelper.GetTopLeftCorner(
                rectangles.Select(rectangle => new Point(rectangle.X, rectangle.Y)));

            var bottomRight = PointHelper.GetBottomRightCorner(
                rectangles.Select(rectangle => new Point(rectangle.Right, rectangle.Bottom)));

            var sideLength = Math.Max(bottomRight.X - topLeft.X, bottomRight.Y - topLeft.Y);
            var radius = sideLength / 2;

            var circleArea = Math.PI * radius * radius;
            var rectanglesArea = rectangles
                .Select(rectangle => rectangle.Width * rectangle.Height)
                .Sum();

            var density = rectanglesArea / circleArea;
            return density;
        }

        private static void AssertHaveNoIntersection(IReadOnlyList<Rectangle> rectangles)
        {
            for (var i = 0; i < rectangles.Count; i++)
                for (var j = i + 1; j < rectangles.Count; j++)
                    Assert.False(
                        rectangles[i].IntersectsWith(rectangles[j]),
                        $"{rectangles[i]} intersects with {rectangles[j]}");
        }

        private ICloudLayouter CreateMockLayouter(ICloudLayouter layouterToMock)
        {
            resultingRectangles = new List<Rectangle>();
            var mockLayouter = new Mock<ICloudLayouter>();
            mockLayouter.Setup(mock => mock.PutNextRectangle(It.IsAny<Size>()))
                .Returns<Size>(
                    size =>
                    {
                        var rectangle = layouterToMock.PutNextRectangle(size);
                        resultingRectangles.Add(rectangle);
                        return rectangle;
                    });

            return mockLayouter.Object;
        }
    }
}