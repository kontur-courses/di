using System.Drawing;
using TagCloudTests;

namespace TagCloud;

public static class Extensions
{
    public static void DrawRectangles(this Graphics graphics, IColorSelector selector, Rectangle[] rectangles, int lineWidth = 1)
    {
        var pen = new Pen(selector.PickColor(), lineWidth);
        foreach (var rectangle in rectangles)
        {
            graphics.DrawRectangle(pen, rectangle);
            pen.Color = selector.PickColor();
        }
    }
}