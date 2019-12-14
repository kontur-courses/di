using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.Visualizer;
using FluentAssertions;
using NUnit.Framework;
using FakeItEasy;

namespace TagsCloudContainer.NewTests
{
    class TagCloudVisualizerTests
    {
        private IVisualizerSettings settings;
        private IVisualizer visualizer;

        [SetUp]
        public void CreateVisualizer()
        {
            var font = new Font(FontFamily.GenericMonospace, 10, FontStyle.Regular, GraphicsUnit.Pixel);
            settings = A.Fake<IVisualizerSettings>();
            A.CallTo(() => settings.GetFont(null))
                .WithAnyArguments()
                .Returns(font);

            A.CallTo(() => settings.GetBrush(null))
                .WithAnyArguments()
                .Returns(Brushes.Black);

            A.CallTo(() => settings.BackgroundBrush)
                .WithAnyArguments()
                .Returns(Brushes.White);

            visualizer = new TagCloudVisualizer(settings);
        }

        [Test]
        public void DrawImage_ShouldReturnImageWithCorrectSize()
        {
            var size = new Size(100, 100);
            var wordRectangles = new List<WordRectangle>
            {
                new WordRectangle("земля", new Rectangle(3, 3, 10, 10)),
                new WordRectangle("привет", new Rectangle(15, 15, 20, 20))
            };

            var image = visualizer.DrawImage(wordRectangles, size);
            image.Size.Should().Be(size);
        }

        [Test]
        public void DrawImage_ShouldCallSettingsMethodsForEveryTag()
        {
            var size = new Size(100, 100);
            var wordRectangles = new List<WordRectangle>
            {
                new WordRectangle("земля", new Rectangle(3, 3, 10, 10)),
                new WordRectangle("привет", new Rectangle(15, 15, 20, 20))
            };

            var image = visualizer.DrawImage(wordRectangles, size);
            A.CallTo(() => settings.GetFont(null))
                .WithAnyArguments()
                .MustHaveHappenedTwiceExactly();
            A.CallTo(() => settings.GetBrush(null))
                .WithAnyArguments()
                .MustHaveHappenedTwiceExactly();
        }
    }
}
