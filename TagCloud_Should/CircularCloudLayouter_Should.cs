using System;
using System.Drawing;
using NUnit.Framework;
using FluentAssertions;
using TagCloud.CloudLayouter.CircularLayouter;
using TagCloud.Visualization;

namespace TagCloud_Should
{
    [TestFixture]
    public class CircularCloudLayouter_Should
    {
        private CircularCloudLayouter layouter;
        private SpiralSettings spiralSettings;

        [SetUp]
        public void SetNewSettings()
        {
            spiralSettings = new SpiralSettings();
        }

        [TestCase(-1, -1)]
        [TestCase(-1, 1)]
        [TestCase(1, -1)]
        [TestCase(0, 0)]
        [TestCase(0, 1)]
        [TestCase(1, 0)]
        public void NonPositiveScreenSize_ThrowsException(int width, int height)
        {
            var imageSettings = new ImageSettings {ImageSize = new Size(width, height)};
            Action action = () =>
                layouter = new CircularCloudLayouter(new ArchimedeanSpiral(spiralSettings), imageSettings);
            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void PutNextRectangle_FirstRect_ShouldBeInCenterWithSomeBias()
        {
            var imageSettings = new ImageSettings {ImageSize = new Size(600, 600)};
            layouter = new CircularCloudLayouter(new ArchimedeanSpiral(spiralSettings), imageSettings);
            var rectangle = layouter.TryPutNextRectangle(new Size(10, 3), out var outRectangle)
                ? outRectangle
                : Rectangle.Empty;
            rectangle.Location.Should().BeEquivalentTo(new Point(7, 3));
        }

        [TestCase(-1, -1)]
        [TestCase(-1, 1)]
        [TestCase(1, -1)]
        [TestCase(0, 0)]
        [TestCase(0, 1)]
        [TestCase(1, 0)]
        public void PutNextRectangle_NonPositiveRectSize_ThrowsException(int width, int height)
        {
            var imageSettings = new ImageSettings {ImageSize = new Size(600, 600)};
            layouter = new CircularCloudLayouter(new ArchimedeanSpiral(spiralSettings), imageSettings);
            Action action = () =>
                layouter
                    .TryPutNextRectangle(new Size(width, height), out _);
            action.Should().Throw<ArgumentException>();
        }

        [TestCase(101, 50, 100, 100)]
        [TestCase(100, 101, 200, 100)]
        [TestCase(101, 51, 100, 50)]
        public void PutNextRectangle_SizeBiggerThanScreen_ThrowsException(int width, int height, int screenWidth,
            int screenHeight)
        {
            var imageSettings = new ImageSettings {ImageSize = new Size(screenWidth, screenHeight)};
            layouter = new CircularCloudLayouter(new ArchimedeanSpiral(spiralSettings), imageSettings);
            Action action = () =>
                layouter
                    .TryPutNextRectangle(new Size(width, height), out _);
            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void PutNextRectangle_PositionOutOfScreen_ReturnsFalse()
        {
            var imageSettings = new ImageSettings {ImageSize = new Size(100, 100)};
            layouter = new CircularCloudLayouter(new ArchimedeanSpiral(spiralSettings), imageSettings);
            for (var i = 0; i < 50; i++)
                layouter.TryPutNextRectangle(new Size(10, 10), out _);
            layouter.TryPutNextRectangle(new Size(10, 10), out _).Should().BeFalse();
        }
    }
}