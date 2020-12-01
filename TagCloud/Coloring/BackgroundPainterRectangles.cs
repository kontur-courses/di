using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.Coloring
{
    public class BackgroundPainterRectangles : IBackgroundPainter
    {
        private readonly Random random = new Random();
        public void Draw(List<Tuple<string, Rectangle>> tags, ICanvas canvas, Graphics graphics)
        {
            foreach (var (str, rectangle) in tags)
            {
                DrawAndFillRectangle(rectangle, graphics);
            }
        }
        
        public void DrawAndFillRectangle(Rectangle rectangle, Graphics graphics)
        {
            var color = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
            var brush = new SolidBrush(color);
            graphics.FillRectangle(brush, rectangle);
        }
    }
}