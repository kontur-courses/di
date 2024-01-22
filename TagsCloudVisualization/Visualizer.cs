using System.Drawing;

namespace TagsCloudVisualization;

public static class Visualizer
{
    public static Bitmap Visualize(IList<Rectangle> rectangles, int bitmapWidth, int bitmapHeight)
    {
        var bitmap = new Bitmap(bitmapWidth, bitmapHeight);
        var graphics = Graphics.FromImage(bitmap);

        foreach (var rect in rectangles)
        {
            graphics.DrawRectangle(new Pen(Color.Blue), rect);
        }

        return bitmap;
    }
}
