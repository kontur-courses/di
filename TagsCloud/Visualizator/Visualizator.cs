using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace TagsCloudVisualization
{
    public static class Visualizator
    {
        public static Color BackgroundColor = Color.AliceBlue;
        public static Color ObjectsColor = Color.Blue;

        public static void VizualizeToFile(
            this Dictionary<string, Rectangle> source,
            int width,
            int height,
            string path)
        {
            using (var bitmap = new Bitmap(width * 2 + 2, height * 2 + 2))
            {
                var sourceToDraw = source
                    .Select(TranslateCenterByHalfSize)
                    .ToDictionary(t => t.Item1, f => f.Item2);
                
                DrawWords(bitmap, sourceToDraw, width, height);
                bitmap.Save(path, ImageFormat.Png);
            }
        }

        private static void DrawWords(
            Bitmap bitmap,
            Dictionary<string, RectangleF> source,
            int width,
            int height)
        {
            var font = new Font("Arial", 16);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.TranslateTransform(width, height);
                graphics.Clear(BackgroundColor);
                using (var pen = new Pen(ObjectsColor))
                {
                    foreach (var pair in source)
                    {
                        graphics.DrawString(pair.Key, font, pen.Brush, pair.Value);
                    }
                }
            }
        }

        public static Tuple<string, RectangleF> TranslateCenterByHalfSize(KeyValuePair<string, Rectangle> pair)
        {
            var size = pair.Value.Size;
            var centerOffset = new Point(size.Width / 2, size.Height / 2);
            var newCenter = pair.Value.Center - centerOffset;
            var newRectF = new Rectangle(newCenter, size).ToRectangleF();
            return new Tuple<string, RectangleF>(pair.Key, newRectF);
        }
    }
}
