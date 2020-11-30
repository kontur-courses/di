using System;
using System.Drawing;

namespace TagCloud.Coloring
{
    public class PainterColoringByLocation : IPainter
    {
        private Brush textBrush = new SolidBrush(Color.Black);
        public void DrawAndFillRectangle(Rectangle rectangle, Graphics graphics)
        {
            var brushColor = Color.FromArgb(Math.Abs(rectangle.X) % 255,
                Math.Abs(rectangle.Y) % 255, 100);
            var brush = new SolidBrush(brushColor);
            graphics.FillRectangle(brush, rectangle);
        }

        public void DrawString(Rectangle rectangle, string str, string fontFamily, Graphics graphics)
        {
            var x = rectangle.X - (rectangle.Height / 4);
            var y = rectangle.Y - (rectangle.Height / 2);
            if (rectangle.Height < 2)
                return;
            graphics.DrawString(str, new Font(fontFamily,rectangle.Height), textBrush, x, y);
        }
    }
}