using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using TagsCloudVisualizationTests.Interfaces;

namespace TagsCloudVisualizationTests.TestingLibrary
{
    public static class VisualizerTestHelper
    {
        public static void AssertBitmap(IVisualizer visualizer, Size bitmapSize, List<Point> bitmapPoints, Color color)
        {
            var expected = new Bitmap(bitmapSize.Width, bitmapSize.Height);
            bitmapPoints.ForEach(point => expected.SetPixel(point.X, point.Y, color));
            var output = new VisualOutput(visualizer);

            var actual = output.DrawToBitmap();

            actual.ToEnumerable().Should().Equal(expected.ToEnumerable());
        }
    }
}