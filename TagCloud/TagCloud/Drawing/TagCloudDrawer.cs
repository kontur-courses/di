using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using TagCloud.Extensions;
using TagCloud.TextProcessing;

[assembly: InternalsVisibleTo("TagsCloudVisualization_Test")]
namespace TagCloud.Drawing
{
    internal class Drawer : IDrawer
    {
        public Bitmap Draw(IDrawerOptions options, List<Word> words)
        {
            var colors = options.WordColors.ToList();
            var center = options.Center;
            using var backgroundBrush = new SolidBrush(options.BackgroundColor);

            var rectangles = words.Select(w => w.Rectangle).ToList();
            var bitmapSize = GetCanvasSize(rectangles, center);
            var bitmap = new Bitmap(bitmapSize.Width, bitmapSize.Height);
            var bitmapCenter = new Point(bitmap.Width / 2, bitmap.Height / 2);

            var rnd = new Random(Seed: 100);
            using var g = Graphics.FromImage(bitmap);
            g.FillRectangle(backgroundBrush, 0, 0, bitmapSize.Width, bitmapSize.Height);
            foreach (var word in words)
            {
                var rect = word.Rectangle;
                var color = colors == null || colors.Count == 0
                    ? Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256))
                    : colors[rnd.Next(colors.Count)];
                var brush = new SolidBrush(Color.FromArgb(255, color));
                rect.Offset(new Point(bitmapCenter.X - center.X, bitmapCenter.Y - center.Y));
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
