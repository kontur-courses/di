using System;
using System.Drawing;

namespace TagCloud.Coloring
{
    public class PainterRandomColoring : IPainter
    {
        private readonly Random random = new Random();
        private Brush textBrush = new SolidBrush(Color.Black);
        public void DrawAndFillRectangle(Rectangle rectangle, Graphics graphics)
        {
            var color = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
            var brush = new SolidBrush(color);
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