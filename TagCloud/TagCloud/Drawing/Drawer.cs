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
            var bitmap = GetClearBitmap(options, words.Select(w => w.Rectangle));

            using var g = Graphics.FromImage(bitmap);
            _palette
                .WithWordColors(options.WordColors.ToList())
                .WithBackGroundColor(options.BackgroundColor);
            using var backgroundBrush = new SolidBrush(_palette.BackgroundColor);
            g.FillRectangle(backgroundBrush, 0, 0, bitmap.Width, bitmap.Height);

            var bitmapCenter = new Point(bitmap.Width / 2, bitmap.Height / 2);
            var offsetPoint = new Point(bitmapCenter.X - options.Center.X, bitmapCenter.Y - options.Center.Y);
            foreach (var word in words)
            {
                var rectangle = word.Rectangle;
                using var brush = new SolidBrush(_palette.GetNextColor());
                rectangle.Offset(offsetPoint);
                g.DrawString(word.Text, word.Font, brush, rectangle.Location);
            }
            return bitmap;
        }

        private static Bitmap GetClearBitmap(IDrawerOptions options, IEnumerable<Rectangle> rectangles)
        {
            var bitmapSize = GetCanvasSize(rectangles, options.Center);
            return new Bitmap(bitmapSize.Width, bitmapSize.Height);
        }

        private static Size GetCanvasSize(IEnumerable<Rectangle> rectangles, Point center)
        {
            var union = rectangles.First().UnionRange(rectangles);
            var distances = union.GetDistancesToInnerPoint(center);
            var horizontalIncrement = Math.Abs(distances[0] - distances[2]);
            var verticalIncrement = Math.Abs(distances[1] - distances[3]);
            union.Inflate(horizontalIncrement, verticalIncrement);
            return new Size((int) (union.Width * 1.1), (int) (union.Height * 1.1));
        }
    }
}