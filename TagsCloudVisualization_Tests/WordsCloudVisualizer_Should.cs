using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;

namespace TagsCloudVisualization_Tests
{
    [TestFixture]
    public class WordsCloudVisualizer_Should
    {
        [Test]
        public void DrawRectangles_BeCorrectSize()
        {
            var size = new Size(50, 50);
            var visualizer = new WordsCloudVisualizer(new Palette(Color.DimGray, Brushes.FloralWhite), size);
            visualizer.Draw(Enumerable.Empty<Word>().ToList()).Width.Should().Be(50);
            visualizer.Draw(Enumerable.Empty<Word>().ToList()).Height.Should().Be(50);
        }
    }
}