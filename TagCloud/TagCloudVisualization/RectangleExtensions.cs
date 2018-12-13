using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace TagCloudVisualization
{
    public static class RectangleExtensions
    {
        public static int Area(this Rectangle rectangle) => rectangle.Width * rectangle.Height;

        public static Size GetUnitedSize(this IEnumerable<Rectangle> rectangles)
        {
            rectangles = rectangles.ToList();
            var maxX = rectangles.Max(rect => rect.Right);
            var minX = rectangles.Min(rect => rect.Left);
            var maxY = rectangles.Max(rect => rect.Bottom);
            var minY = rectangles.Min(rect => rect.Top);

            var width = maxX - minX;
            var height = maxY - minY;
            return new Size(width, height);
        }

        public static void DrawRectangles(
            this IEnumerable<Rectangle> rectangles,
            Point center,
            string name = "image",
            int expandPercent = 100)
        {
            var path = Path.Combine("..", "..", name.Replace(Path.AltDirectorySeparatorChar, '_') + ".png");

            var rectangleList = rectangles.ToList();
            var cloudSize = rectangleList.GetUnitedSize();
            var width = cloudSize.Width;
            var height = cloudSize.Height;
            width *= expandPercent / 100;
            height *= expandPercent / 100;

            using (var image = new Bitmap(width, height))
            {
                using (var graphics = Graphics.FromImage(image))
                {
                    foreach (var rectangle in rectangleList)
                    {
                        var imageCenter = new Point(width / 2, height / 2) - center;
                        rectangle.Offset(imageCenter);
                        graphics.DrawRectangle(new Pen(Color.LimeGreen, 5), rectangle);
                        graphics.FillRectangle(Brushes.White, rectangle);
                    }
                }

                image.Save(path);
            }
        }
    }
}
