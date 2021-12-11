using System.Drawing;

namespace TagsCloudVisualization.Extensions
{
    public static class GraphicsExtensions
    {
        public static void DrawRectangle(this Graphics graphics, Pen pen, RectangleF rectangle)
        {
            graphics.DrawRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }
    }
}