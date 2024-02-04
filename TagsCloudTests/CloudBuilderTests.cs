using FluentAssertions;
using System.Drawing;
using TagsCloudContainer.SettingsClasses;
using TagsCloudVisualization;

namespace TagsCloudTests
{
    internal class CloudBuilderTests
    {
        private TagsCloudLayouter sut;
        private List<(string, int)> words;
        private CloudDrawingSettings drawingSettings;

        [SetUp]
        public void Setup()
        {
            drawingSettings = new CloudDrawingSettings
            {
                Size = new(1000, 1000),
                FontFamily = new("Arial"),
                FontSize = 12
            };

            words = new() { ("TestWord1", 1), ("TestWord2", 2), ("TestWord3", 3) };

            sut = new TagsCloudLayouter();
            sut.Initialize(drawingSettings, words);
        }

        [Test]
        public void Initialize_WithValidSettings_ShouldNotThrowException()
        {
            // Arrange
            var layouter = new TagsCloudLayouter();

            // Act & Assert
            Assert.DoesNotThrow(() => layouter.Initialize(drawingSettings, words));
        }

        [Test]
        public void Initialize_WithInvalidSize_ShouldThrowArgumentException()
        {
            // Arrange
            var layouter = new TagsCloudLayouter();
            var invalidDrawingSettings = new CloudDrawingSettings
            {
                Size = new Size(-1, 1000)
            };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => layouter.Initialize(invalidDrawingSettings, words));
        }

        [Test]
        public void GetTextImages_ShouldContainCorrectNumberOfRectangles()
        {
            // Arrange
            var expectedCount = words.Count;

            // Act
            var actualCount = sut.GetTextImages().Count();

            // Assert
            actualCount.Should().Be(expectedCount);
        }

        [Test]
        public void GetTextImages_ShouldReturnCorrectTextImages()
        {
            // Arrange
            var layouter = new TagsCloudLayouter();
            layouter.Initialize(drawingSettings, words);

            // Act
            var textImages = layouter.GetTextImages().ToList();

            // Assert
            textImages.Should().ContainSingle(x => x.Text == "TestWord1");
            textImages.Should().ContainSingle(x => x.Text == "TestWord2");
            textImages.Should().ContainSingle(x => x.Text == "TestWord3");
        }

        [Test]
        public void GetTextImages_ShouldNotContainOverlappingTextImages()
        {
            // Arrange
            var layouter = new TagsCloudLayouter();
            layouter.Initialize(drawingSettings, words);


            // Act
            var textImages = layouter.GetTextImages().Select(x => new Rectangle(x.Position, x.Size));
            var rectangles = textImages
                 .SelectMany((x, i) => textImages.Skip(i + 1), Tuple.Create)
                 .Where(x => x.Item1.IntersectsWith(x.Item2));


            // Assert
            rectangles.Should().BeEmpty();
        }
    }
}
