using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization.Printing
{
    public class RectanglePrinter : IPrinter<Rectangle>
    {
        private const int Margin = 100;
        
        public Bitmap GetBitmap(IColorScheme colorScheme, IEnumerable<Rectangle> objects, Size? bitmapSize = null)
        {
            var rects = objects.ToList();
            if (!rects.Any())
                throw new ArgumentException($"rectangle list is empty");

            var firstRectangle = rects.First();
            var actualCenter = new Point(firstRectangle.X + firstRectangle.Width / 2, 
                firstRectangle.Y + firstRectangle.Height / 2);

            var bmp = new Bitmap(
                Math.Abs(rects.Max(x => x.Right) - rects.Min(x => x.Left)) + Margin,
                Math.Abs(rects.Max(x => x.Bottom) - rects.Min(x => x.Top)) + Margin
            );

            var centersDelta = new Size(actualCenter.X - bmp.Width / 2, actualCenter.Y - bmp.Height / 2);

            using var graphics = Graphics.FromImage(bmp);
            DrawRectangles(colorScheme, graphics, rects.Select(rect => rect.Move(-centersDelta.Width, -centersDelta.Height)));

            return bmp;
        }
        
        private void DrawRectangles(IColorScheme colorScheme, Graphics graphics, IEnumerable<Rectangle> rectangles)
        {
            foreach (var rectangle in rectangles)
            {
                using var pen = new Pen(Brushes.Black, 1);
                pen.Color = colorScheme.GetColorBy(rectangle.Size);
                graphics.DrawRectangle(pen, rectangle);
            }
        }
    }
}