using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public static class Painter
    {
        private const int Margin = 100;
        
        public static Bitmap GetBitmapWithRectangles(IEnumerable<Rectangle> rectangles)
        {
            var rects = rectangles.ToList();
            if (!rects.Any())
                throw new ArgumentException($"rectangle list is empty");

            var firstRectangle = rects.First();
            var actualCenter = new Point(firstRectangle.X + firstRectangle.Width / 2, 
                firstRectangle.Y + firstRectangle.Height / 2);

            var bmp = new Bitmap(
                Math.Abs(rects.Max(x => x.Right) - rects.Min(x => x.Left)) + Margin,
                Math.Abs(rects.Max(x => x.Bottom) - rects.Min(x => x.Top)) + Margin
            );

            var centersDelta = new Size(actualCenter.X - bmp.Width / 2, 
                actualCenter.Y - bmp.Height / 2);

            using var graphics = Graphics.FromImage(bmp);
            DrawRectangles(graphics, rects.Select(rect => rect.Move(-centersDelta.Width, -centersDelta.Height)));

            return bmp;
        }
        
        public static Bitmap GetBitmapWithText(IEnumerable<(string, float, Rectangle)> rectangles)
        {
            var rects = rectangles.ToList();
            if (!rects.Any())
                throw new ArgumentException($"rectangle list is empty");

            var firstRectangle = rects.First();
            var actualCenter = new Point(firstRectangle.Item3.X + firstRectangle.Item3.Width / 2, 
                                         firstRectangle.Item3.Y + firstRectangle.Item3.Height / 2);

            var bmp = new Bitmap(
                Math.Abs(rects.Max(x => x.Item3.Right) - rects.Min(x => x.Item3.Left)) + Margin,
                Math.Abs(rects.Max(x => x.Item3.Bottom) - rects.Min(x => x.Item3.Top)) + Margin
                );

            var centersDelta = new Size(actualCenter.X - bmp.Width / 2, 
                                        actualCenter.Y - bmp.Height / 2);

            using var graphics = Graphics.FromImage(bmp);
            DrawText(graphics, rects.Select(x => (x.Item1, x.Item2, x.Item3.Move(-centersDelta.Width, -centersDelta.Height))));
            // DrawRectangles(graphics, rects.Select(x => x.Item3.Move(-centersDelta.Width, -centersDelta.Height)));

            return bmp;
        }
        
        private static void DrawText(Graphics graphics, IEnumerable<(string, float, Rectangle)> rectangles)
        {
            var rnd = new Random();

            foreach (var (word, size, rectangle) in rectangles)
            {
                var rf = new RectangleF(rectangle.X, rectangle.Y - rectangle.Height / 2, rectangle.Width, rectangle.Height * 2);
                Console.WriteLine(word);
                graphics.DrawString(word, 
                    new Font(SystemFonts.DefaultFont.FontFamily, size, FontStyle.Regular), 
                    new SolidBrush(Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255))), rf);
                graphics.DrawRectangle(new Pen(Color.Blue), new Rectangle((int)rf.X, (int)rf.Y, (int)rf.Width, (int)rf.Height));
            }
        }

        private static void DrawRectangles(Graphics graphics, IEnumerable<Rectangle> rectangles)
        {
            var rnd = new Random();

            foreach (var rectangle in rectangles)
            {
                using var pen = new Pen(Brushes.Black, 1);
                pen.Color = Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
                graphics.DrawRectangle(pen, rectangle);
            }
        }
    }
}