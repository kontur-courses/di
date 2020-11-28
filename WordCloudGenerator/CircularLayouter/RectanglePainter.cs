using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace WordCloudGenerator.CircularLayouter
{
    public static class RectanglePainter
    {
        public static void SaveToFile(Bitmap image, string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException();

            image.Save(path);
        }

        private static Bitmap DrawRectangles(IEnumerable<RectangleF> rectangles, Color penColor)
        {
            var rects = rectangles.ToArray();
            if (rects.Length == 0)
                throw new ArgumentException("Collection contains 0 rectangles");

            var maxX = (int) rects.Max(rect => rect.X);
            var minX = (int) rects.Min(rect => rect.X);
            var maxY = (int) rects.Max(rect => rect.Y);
            var minY = (int) rects.Min(rect => rect.Y);

            var width = maxX - minX;
            var height = maxY - minY;

            var shiftVector = new Point(-minX, -minY);

            using var bitmap = new Bitmap(width + 100, height + 100);
            using var pen = new Pen(penColor);
            using var graphics = Graphics.FromImage(bitmap);

            graphics.DrawRectangles(pen, rects.Select(rectangle => rectangle.Shift(shiftVector)).ToArray());
            return bitmap;
        }
    }
}