using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using TagCloud.Extensions;


namespace TagCloud
{
    public class TagCloudDrawer
    {
        private readonly Point _center;
        private readonly List<Color> _colors;

        public TagCloudDrawer(Point center, List<Color> colors = null)
        {
            _center = center;
            _colors = colors;
        }

        public Bitmap Draw(List<Rectangle> rectangles)
        {
            var bitmapSize = GetCanvasSize(rectangles, _center);
            var bitmap = new Bitmap(bitmapSize.Width, bitmapSize.Height);
            var bitmapCenter = new Point(bitmap.Width / 2, bitmap.Height / 2);

            var rnd = new Random(Seed: 100);
            using var g = Graphics.FromImage(bitmap);
            g.FillRectangle(Brushes.White, 0, 0, bitmapSize.Width, bitmapSize.Height);
            foreach (var rect in rectangles)
            {
                var color = _colors == null
                    ? Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256))
                    : _colors[rnd.Next(_colors.Count - 1)];
                var pen = new Pen(color, 0) { Alignment = PenAlignment.Inset };
                var brush = new SolidBrush(Color.FromArgb(25, color));
                rect.Offset(new Point(bitmapCenter.X - _center.X, bitmapCenter.Y - _center.Y));
                g.DrawRectangle(pen, rect);
                g.FillRectangle(brush, rect);
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
