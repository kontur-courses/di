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
            var center = new Point(0, 0);
            var spiral = new Spiral(0.0005, 0);
            var layouter = new CircularCloudLayouter(new LayouterSettings(center, spiral));
            var wordsCloudLayouter = new WordsCloudLayouter(layouter, new FontSettings(new FontFamily("Times New Roman"), FontStyle.Bold));
            var words = wordsCloudLayouter.LayWords(new List<(string, int)>{("letter", 4)}).ToList();
            var size = new Size(layouter.Radius * 2, layouter.Radius * 2);
            var visualizer = new WordsCloudVisualizer(new Palette(Color.DimGray, Brushes.FloralWhite), size);
            visualizer.Draw(words).Width.Should().Be(layouter.Radius * 2);
            visualizer.Draw(words).Height.Should().Be(layouter.Radius * 2);
        }
    }
}