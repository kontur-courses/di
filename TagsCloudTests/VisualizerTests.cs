using FluentAssertions;
using System.Drawing;
using TagsCloudContainer;
using TagsCloudContainer.Drawer;
using TagsCloudContainer.SettingsClasses;

namespace TagsCloudTests
{
    [TestFixture]
    public class VisualizerTests
    {
        [Test]
        public void Draw_WithValidParameters_ShouldReturnCorrectImage()
        {
            // Arrange
            var size = new Size(100, 100);
            var words = new List<(string, int)>
            {
                ("TestWord1", 1),
                ("TestWord2", 2)
            };
            var drawingSettings = new CloudDrawingSettings
            {
                Size = size,
                FontFamily = new FontFamily("Arial"),
                FontSize = 12
            };

            // Act
            var textImages = Visualizer.Draw(
                size,
                words.Select(x => new TextImage(x.Item1,
                                  new Font(drawingSettings.FontFamily, drawingSettings.FontSize + x.Item2),
                                    new Size(x.Item2, x.Item2), Color.White, new Point(0, 0))).ToList());

            // Assert
            textImages.Should().NotBeNull();
            textImages.Width.Should().Be(size.Width);
            textImages.Height.Should().Be(size.Height);
        }

        [Test]
        public void Draw_WithEmptyParameters_ShouldReturnImage()
        {
            // Arrange
            var size = new Size(100, 100);
            var drawingSettings = new CloudDrawingSettings
            {
                Size = size,
                FontFamily = new FontFamily("Arial"),
                FontSize = 12
            };

            // Act
            var image = Visualizer.Draw(size, Enumerable.Empty<TextImage>());

            // Assert
            image.Should().NotBeNull();
            image.Width.Should().Be(size.Width);
            image.Height.Should().Be(size.Height);
        }
    }
}