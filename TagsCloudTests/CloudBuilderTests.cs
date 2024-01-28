using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.Drawing;
using TagsCloudContainer;
using TagsCloudContainer.SettingsClasses;
using TagsCloudVisualization;

namespace TagsCloudTests
{
    internal class CloudBuilderTests
    {
        private ServiceProvider serviceProvider;
        private TagsCloudLayouter sut;
        private List<(string, int)> words;

        [SetUp]
        public void Setup()
        {
            var services = DependencyInjectionConfig.AddCustomServices(new ServiceCollection());
            serviceProvider = services.BuildServiceProvider();

            var center = new Point(100, 100);
            var pointsProvider = new SpiralPointsProvider();
            var drawingSettings = serviceProvider.GetService<CloudDrawingSettings>();

            drawingSettings.Size = new Size(center.X * 2, center.Y * 2);
            drawingSettings.PointsProvider = pointsProvider;
            words = new() { ("TestWord1", 1), ("TestWord2", 2), ("TestWord3", 3) };

            sut = new TagsCloudLayouter();
            sut.Initialize(drawingSettings, words);
            sut.ToImage();
        }

        [Test]
        public void ToImage_ShouldReturnCorrectImage()
        {
            // Arrange
            var expectedImage = new Bitmap(200, 200);

            // Act
            var actualImage = sut.ToImage();

            // Assert
            actualImage.Should().NotBeNull();
            actualImage.Should().BeOfType<Bitmap>();
            actualImage.Width.Should().Be(expectedImage.Width);
            actualImage.Height.Should().Be(expectedImage.Height);
        }

        [Test]
        public void ToImage_ShouldContainCorrectNumberOfRectangles()
        {
            // Arrange
            var expectedCount = words.Count();

            // Act
            var actualCount = sut.Cloud.Count;

            // Assert
            actualCount.Should().Be(expectedCount);
        }

        [Test]
        public void ToImage_ShouldNotOverlapRectangles()
        {
            // Arrange
            var expectedNoOverlap = true;

            // Act
            var actualNoOverlap = true;
            foreach (var rectangle1 in sut.Cloud)
            {
                foreach (var rectangle2 in sut.Cloud)
                {
                    if (rectangle1 == rectangle2)
                    {
                        continue;
                    }
                    var isIntersect = rectangle1.IntersectsWith(rectangle2);
                    if (isIntersect)
                    {
                        actualNoOverlap = false;
                        break;
                    }
                    if (!actualNoOverlap)
                    {
                        break;
                    }
                }

            }

            // Assert
            actualNoOverlap.Should().Be(expectedNoOverlap);
        }
    }
}
