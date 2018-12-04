using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace TagsCloudVisualization
{
    public static class Visualizator
    {
        public static readonly Color BackgroundColor = Color.AliceBlue;
        public static readonly Color ObjectsColor = Color.Blue;

        public static void VizualizeToFile(
            this IEnumerable<Rectangle> rectangles,
            int width,
            int height,
            string path)
        {
            using (var bitmap = new Bitmap(width * 2 + 2, height * 2 + 2))
            {
                var rectsToDraw = rectangles
                    .Select(TranslateCenterByHalfSize)
                    .Select(r => r.ToRectangleF())
                    .ToArray();
                
                DrawRectangles(bitmap, rectsToDraw, width, height);
                bitmap.Save(path, ImageFormat.Png);
            }
        }

        private static void DrawRectangles(
            Bitmap bitmap, 
            RectangleF[] rectangles,
            int width,
            int height)
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.TranslateTransform(width, height);
                graphics.Clear(BackgroundColor);
                using (var pen = new Pen(ObjectsColor))
                {
                    graphics.DrawRectangles(pen, rectangles);
                }
            }
        }

        public static Rectangle TranslateCenterByHalfSize(Rectangle rectangle)
        {
            var size = rectangle.Size;
            var centerOffset = new Point(size.Width / 2, size.Height / 2);
            var newCenter = rectangle.Center - centerOffset;
            return new Rectangle(newCenter, size);
        }
    }
}
