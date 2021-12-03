using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace TagsCloudContainer
{
    public static class CloudImageGenerator
    {
        private const int AddedImageSize = 300;

        public static string CreateImage(Cloud cloud, Size desiredSize)
        {
            if (!cloud.Rectangles.Any())
                throw new ArgumentException("No rectangles received");
            var imageSize = CalculateCoverageSize(cloud.Rectangles)
                + new Size(AddedImageSize, AddedImageSize);
            var imageOffset = imageSize / 2 - new Size(cloud.Center);
            var scaleX = desiredSize.Width / (float)imageSize.Width;
            var scaleY = desiredSize.Height / (float)imageSize.Height;

            return DrawImage(cloud.Rectangles, desiredSize,
                imageOffset, Path.Combine(Path.GetFullPath(@"..\..\..\"), "layouts"), scaleX, scaleY);
        }

        private static string DrawImage(IReadOnlyCollection<Rectangle> rectangles,
            Size imageSize, Size imageOffset, string path, float scaleX, float scaleY)
        {
            var bm = new Bitmap(imageSize.Width, imageSize.Height);
            var graphics = Graphics.FromImage(bm);
            graphics.ScaleTransform(scaleX, scaleY);
            foreach (var rectangle in rectangles)
            {
                rectangle.Offset(new Point(imageOffset));
                graphics.DrawRectangle(new Pen(Color.DarkOrange, 4), rectangle);
            }

            var savePath = $"{path}\\layout_{DateTime.Now:HHmmssddMM}.bmp";
            bm.Save(savePath, ImageFormat.Bmp);
            return savePath;
        }

        private static Size CalculateCoverageSize(IEnumerable<Rectangle> rectangles)
        {
            var maxX = rectangles.Max(x => x.X + x.Size.Width);
            var minX = rectangles.Min(x => x.X);
            var width = maxX - minX;

            var maxY = rectangles.Max(x => x.Y);
            var minY = rectangles.Min(x => x.Y - x.Size.Height);
            var height = maxY - minY;

            return new Size(width, height);
        }
    }
}
