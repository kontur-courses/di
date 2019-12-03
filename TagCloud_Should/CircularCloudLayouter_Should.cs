using System;
using System.Drawing;
using NUnit.Framework;
using FluentAssertions;
using TagCloud;
using TagCloud.CloudLayouter;

namespace TagCloud_Should
{
    [TestFixture]
    public class CircularCloudLayouter_Should
    {
        private CircularCloudLayouter layouter;
        private SpiralSettings spiralSettings;
        private ImageSettings imageSettings;

        [SetUp]
        public void SetNewSettings()
        {
            spiralSettings = new SpiralSettings();
            imageSettings = new ImageSettings();
        }

        [TestCase(-1, -1)]
        [TestCase(-1, 1)]
        [TestCase(1, -1)]
        [TestCase(0, 0)]
        [TestCase(0, 1)]
        [TestCase(1, 0)]
        public void CircularCloudLayouter_NonPositiveScreenSize_ThrowsException(int width, int height)
        {
            imageSettings.Width = width;
            imageSettings.Height = height;
            Action action = () =>
                layouter = new CircularCloudLayouter(new ArchimedeanSpiral(spiralSettings), imageSettings);
            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void PutNextRectangle_FirstRect_ShouldBeInCenterWithSomeBias()
        {
            imageSettings.Width = 600;
            imageSettings.Height = 600;
            layouter = new CircularCloudLayouter(new ArchimedeanSpiral(spiralSettings), imageSettings);
            layouter.PutNextRectangle(new Size(10, 3)).Location.Should().BeEquivalentTo(new Point(7, 3));
        }

        [TestCase(-1, -1)]
        [TestCase(-1, 1)]
        [TestCase(1, -1)]
        [TestCase(0, 0)]
        [TestCase(0, 1)]
        [TestCase(1, 0)]
        public void PutNextRectangle_NonPositiveRectSize_ThrowsException(int width, int height)
        {
            imageSettings.Width = 600;
            imageSettings.Height = 600;
            layouter = new CircularCloudLayouter(new ArchimedeanSpiral(spiralSettings), imageSettings);
            Action action = () =>
                layouter
                    .PutNextRectangle(new Size(width, height));
            action.Should().Throw<ArgumentException>();
        }

        [TestCase(101, 50, 100, 100)]
        [TestCase(100, 101, 200, 100)]
        [TestCase(101, 51, 100, 50)]
        public void PutNextRectangle_SizeBiggerThanScreen_ThrowsException(int width, int height, int screenWidth,
            int screenHeight)
        {
            imageSettings.Width = screenWidth;
            imageSettings.Height = screenHeight;
            layouter = new CircularCloudLayouter(new ArchimedeanSpiral(spiralSettings), imageSettings);
            Action action = () =>
                layouter
                    .PutNextRectangle(new Size(width, height));
            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void PutNextRectangle_PositionOutOfScreen_ReturnsFalse()
        {
            imageSettings.Width = 100;
            imageSettings.Height = 100;
            layouter = new CircularCloudLayouter(new ArchimedeanSpiral(spiralSettings), imageSettings);
            for (var i = 0; i < 50; i++)
                layouter.PutNextRectangle(new Size(10, 10));
            layouter.PutNextRectangle(new Size(10, 10)).Size.Should().BeEquivalentTo(Size.Empty);
        }
    }
}