using System;
using System.Drawing;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace TagsCloudVisualization
{
    [TestFixture]
    public class CircularCloudLayouter_should
    {
        private CircularCloudLayouter circularCloudLayouter;

        [SetUp]
        public void SetUp()
        {
            circularCloudLayouter = new CircularCloudLayouter(new Point(0, 0));
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed)
                return;

            var name = $"{TestContext.CurrentContext.Test.FullName}.png";
            var path = Path.Combine(Environment.CurrentDirectory, name);

            var visualizer = new RectangleLayoutVisualizer();
            var image = visualizer.Visualize(new Size(700, 700),
                circularCloudLayouter.PastRectangles.ToArray(), new Point(0, 0));
            new ImageSaver().Save(image, path);

            TestContext.Error.Write($@"Tag cloud visualization saved to file <{path}>");
        }

        [TestCase(0, 0, 100, 100, TestName = "center equal (0,0)")]
        [TestCase(-10, 40, 100, 100, TestName = "center not equal (0,0)")]
        public void PutNextRectangle_InCenter_When(int centerX, int centerY, 
            int rectangleWidth, int rectangleHeight)
        {
            var center = new Point(centerX, centerY);
            circularCloudLayouter = new CircularCloudLayouter(center);

            var rectangleSize = new Size(rectangleWidth, rectangleHeight);
            var leftTopLocation = new Point(
                (int)Math.Ceiling(center.X - rectangleSize.Width / 2d),
                (int)Math.Ceiling(center.Y - rectangleSize.Height / 2d)
            );
            var expectedLocation = new Rectangle(leftTopLocation, rectangleSize);

            var rectanglLocation = circularCloudLayouter.PutNextRectangle(rectangleSize);

            rectanglLocation.Should().Be(expectedLocation);
        }

        [Test]
        public void PutNextRectangle_WithoutIntersectionsWithPastRectangles()
        {
            var firstRectangleSize = new Size(100, 100);
            var secondRectangleSize = new Size(100, 100);

            var firstRectangleLocation = circularCloudLayouter
                .PutNextRectangle(firstRectangleSize);
            var secondRectangleLocation = circularCloudLayouter
                .PutNextRectangle(secondRectangleSize);

            firstRectangleLocation
                .IntersectsWith(secondRectangleLocation)
                .Should().BeFalse();
        }

        [Test, MaxTime(1000)]
        public void PutNextRectangle_ShouldNotCycle()
        {
            var rectangleLocations = Enumerable.Repeat(new Size(30, 30), 40);
            var rectanglesLayout = rectangleLocations
                .Select(circularCloudLayouter.PutNextRectangle)
                .ToArray();
        }

        [Test]
        public void PutNextRectangle_ShouldReturnRectangleWithPassedSize()
        {
            var rectangleSize = new Size(100, 10);

            var rectangle = circularCloudLayouter.PutNextRectangle(rectangleSize);

            rectangle.Size.Should().Be(rectangleSize);
        }

        [TestCase(0, 1, TestName = "width equal zero")]
        [TestCase(1, 0, TestName = "height equal zero")]
        [TestCase(1, -1, TestName = "height is negative")]
        [TestCase(-1, 1, TestName = "width is negative")]
        public void PutNextRectangle_ShouldThrowArgumentException_When(
            int width, int height) =>
            Assert.Throws<ArgumentException>(
                () => circularCloudLayouter.PutNextRectangle(new Size(width, height)),
                "rectangle size should be positive");
    }
}
