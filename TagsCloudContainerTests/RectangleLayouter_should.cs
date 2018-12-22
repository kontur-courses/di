using System;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Infrastructure.PointTracks;
using TagsCloudContainer.Visualization;

namespace TagsCloudContainerTests
{
    [TestFixture]
    public class RectangleLayouter_should
    {
        private RectangleLayouter rectangleLayouter;
        private IPointsTrack spiralTrack;
        private const double trackStep = 0.5;

        [SetUp]
        public void SetUp()
        {
            spiralTrack = new SpiralTrack(new Point(0, 0), trackStep);
            rectangleLayouter = new RectangleLayouter(new Point(0, 0),
                spiralTrack);
        }

        [TestCase(0, 0, 100, 100, TestName = "center equal (0,0)")]
        [TestCase(-10, 40, 100, 100, TestName = "center not equal (0,0)")]
        public void PutNextRectangle_InCenter_When(int centerX, int centerY,
            int rectangleWidth, int rectangleHeight)
        {
            var center = new Point(centerX, centerY);
            rectangleLayouter = new RectangleLayouter(center,
                new SpiralTrack(center, trackStep));

            var rectangleSize = new Size(rectangleWidth, rectangleHeight);
            var leftTopLocation = new Point(
                (int) Math.Ceiling(center.X - rectangleSize.Width / 2d),
                (int) Math.Ceiling(center.Y - rectangleSize.Height / 2d)
            );
            var expectedLocation = new Rectangle(leftTopLocation, rectangleSize);

            var rectangleLocation = rectangleLayouter.PutNextRectangle(rectangleSize);

            rectangleLocation.Should().Be(expectedLocation);
        }

        [Test]
        public void PutNextRectangle_WithoutIntersectionsWithPastRectangles()
        {
            var firstRectangleSize = new Size(100, 100);
            var secondRectangleSize = new Size(100, 100);

            var firstRectangleLocation = rectangleLayouter
                .PutNextRectangle(firstRectangleSize);
            var secondRectangleLocation = rectangleLayouter
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
                .Select(rectangleLayouter.PutNextRectangle)
                .ToArray();
        }

        [Test]
        public void PutNextRectangle_ShouldReturnRectangleWithPassedSize()
        {
            var rectangleSize = new Size(100, 10);

            var rectangle = rectangleLayouter.PutNextRectangle(rectangleSize);

            rectangle.Size.Should().Be(rectangleSize);
        }

        [TestCase(0, 1, TestName = "width equal zero")]
        [TestCase(1, 0, TestName = "height equal zero")]
        [TestCase(1, -1, TestName = "height is negative")]
        [TestCase(-1, 1, TestName = "width is negative")]
        public void PutNextRectangle_ShouldThrowArgumentException_When(
            int width, int height) =>
            Assert.Throws<ArgumentException>(
                () => rectangleLayouter.PutNextRectangle(new Size(width, height)),
                "rectangle size should be positive");
    }
}