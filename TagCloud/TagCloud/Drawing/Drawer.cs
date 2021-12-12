using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Extensions;
using TagCloud.TextProcessing;

namespace TagCloud.Drawing
{
    internal class Drawer : IDrawer
    {
        private readonly IPalette _palette;

        public Drawer(IPalette palette)
        {
            _palette = palette;
        }

        public Bitmap Draw(IDrawerOptions options, List<Word> words)
        {
            var center = options.Center;

            var rectangles = words.Select(w => w.Rectangle).ToList();
            var bitmapSize = GetCanvasSize(rectangles, center);
            var bitmap = new Bitmap(bitmapSize.Width, bitmapSize.Height);
            var bitmapCenter = new Point(bitmap.Width / 2, bitmap.Height / 2);
            
            using var g = Graphics.FromImage(bitmap);
            using var backgroundBrush = new SolidBrush(options.BackgroundColor);
            g.FillRectangle(backgroundBrush, 0, 0, bitmapSize.Width, bitmapSize.Height);
            
            var offsetPoint = new Point(bitmapCenter.X - center.X, bitmapCenter.Y - center.Y);
            foreach (var word in words)
            {
                var rect = word.Rectangle;
                var color = _palette
                    .WithColors(options.WordColors.ToList())
                    .GetNextColor();
                using var brush = new SolidBrush(color);
                rect.Offset(offsetPoint);
                g.DrawString(word.Text, word.Font, brush, rect.Location);
            }

            return bitmap;
        }

        private Size GetCanvasSize(List<Rectangle> rectangles, Point center)
        {
            var union = rectangles.First().UnionRange(rectangles);
            var distances = union.GetDistancesToInnerPoint(center);
            var horizontalIncrement = Math.Abs(distances[0] - distances[2]);
            var verticalIncrement = Math.Abs(distances[1] - distances[3]);
            union.Inflate(horizontalIncrement, verticalIncrement);
            return new Size((int)(union.Width * 1.1), (int)(union.Height * 1.1));
        }
    }
}
