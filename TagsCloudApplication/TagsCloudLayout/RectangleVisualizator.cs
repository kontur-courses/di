using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudLayout
{
    public static class RectangleVisualizator
    {
        private readonly static Random random = new Random();

        public static Bitmap GetBitmapFromRectangles(Point center,
            IReadOnlyCollection<Rectangle> rectangles)
        {
            rectangles = rectangles.ToList();

            var maxDistance = (int)Math.Sqrt(center.GetMaxSquaredDistanceTo(rectangles));
            maxDistance += rectangles.Select(rect => Math.Max(rect.Width, rect.Height)).Max();
            var imageSize = new Size(2 * maxDistance, 2 * maxDistance);
            var imageCenter = new Point(maxDistance, maxDistance);

            var bitmap = new Bitmap(imageSize.Width, imageSize.Height);
            var graphics = Graphics.FromImage(bitmap);

            foreach (var rect in rectangles)
            {
                var randomColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                var brush = new SolidBrush(randomColor);
                graphics.FillRectangle(brush, rect.GetShiftedToNewCenter(center, imageCenter));
            }

            return bitmap;
        }
    }
}
