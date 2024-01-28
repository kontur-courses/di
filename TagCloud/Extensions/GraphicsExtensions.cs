using System.Drawing;
using TagCloudTests;

namespace TagCloud.Extensions;

public static class GraphicsExtensions
{
    public static void DrawRectangles(this Graphics graphics, IColorSelector selector, Rectangle[] rectangles, int lineWidth = 1)
    {
        using var pen = new Pen(selector.PickColor(), lineWidth);
        foreach (var rectangle in rectangles)
        {
            graphics.DrawRectangle(pen, rectangle);
            pen.Color = selector.PickColor();
        }
    }
    
    public static void DrawStrings(this Graphics graphics, IColorSelector selector, TextRectangle[] rectangles)
    {
        using var brush = new SolidBrush(selector.PickColor());
        foreach (var rectangle in rectangles)
        {
            graphics.DrawString(rectangle.Text, rectangle.Font, brush, rectangle.X, rectangle.Y);
            brush.Color = selector.PickColor();
        }
    }
}