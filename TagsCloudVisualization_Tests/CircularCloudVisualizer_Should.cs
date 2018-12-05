using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;

namespace TagsCloudVisualization_Tests
{
    [TestFixture]
    public class CircularCloudVisualizer_Should
    {
        [Test]
        public void DrawRectangles_BeCorrectSize()
        {
            var layout = new CircularCloudLayouter(new LayouterSettings(new Point(0,0), new Spiral(0.0005, 0)));
            var rectangles = new List<Rectangle>();
            rectangles.Add(layout.PutNextRectangle(new Size(200, 100)));
            rectangles.Add(layout.PutNextRectangle(new Size(200, 100)));
            var size = new Size(layout.Radius * 2, layout.Radius * 2);
            var visualizer = new CircularCloudVisualizer(new Palette(Color.DimGray, Brushes.FloralWhite), size);
            visualizer.Draw(rectangles).Width.Should().Be(layout.Radius * 2);
            visualizer.Draw(rectangles).Height.Should().Be(layout.Radius * 2); 
        }
    }
}
