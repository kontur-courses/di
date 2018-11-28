using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace TagsCloudVisualization
{
    public class CircularCloudVisualization
    {
        public CircularCloudVisualization(ICloudLayouter cloudLayouter)
        {
            this.cloudLayouter = cloudLayouter;
            bitmapHeight = cloudLayouter.Spiral.Center.Y * 2;
            bitmapWidth = cloudLayouter.Spiral.Center.X * 2;
        }

        private readonly ICloudLayouter cloudLayouter;

        private readonly int bitmapWidth;
        private readonly int bitmapHeight;

        public void SaveCloudLayouter(string bitmapName, string directory)
        {
            var countOfRectangles = cloudLayouter.Rectangles.Count;
            var bitmap = GetBitmapWithRectangles();
            var path = $"{directory}\\..\\..\\Images\\{bitmapName}-{countOfRectangles}.png";

            bitmap.Save(path, ImageFormat.Png);
        }

        private Bitmap GetBitmapWithRectangles()
        {
            var bitmap = new Bitmap(bitmapWidth, bitmapHeight);
            var pen = new Pen(Color.Black);

            var rectangles = cloudLayouter.Rectangles;

            var maxDist = (int)rectangles
                .Select(x => GetDistanceFromRectangleToPoint(x, cloudLayouter.Spiral.Center))
                .Max();

            foreach (var rectangle in rectangles)
            {
                var color = GetColorOfRectangle(rectangle, cloudLayouter.Spiral.Center, maxDist);
                var brush = new SolidBrush(color);

                Graphics.FromImage(bitmap).FillRectangle(brush, rectangle);
                Graphics.FromImage(bitmap).DrawRectangle(pen, rectangle);
            }

            return bitmap;
        }

        private Color GetColorOfRectangle(Rectangle rectangle, Point center, int maxDist)
        {
            var dist = GetDistanceFromRectangleToPoint(rectangle, center);
            var r = (int)(dist / maxDist * 255 * 0.9);
            var g = (int)(dist / maxDist * 255 * 0.7);
            var b = (int)(dist / maxDist * 255 * 0.5);

            return Color.FromArgb(r, g, b);
        }

        private double GetDistanceFromRectangleToPoint(Rectangle rectangle, Point center)
        {
            return Math.Sqrt((GetCenterOfRectangle(rectangle).X - center.X) *
                             (GetCenterOfRectangle(rectangle).X - center.X) +
                             (GetCenterOfRectangle(rectangle).Y - center.Y) *
                             (GetCenterOfRectangle(rectangle).Y - center.Y));
        }

        private Point GetCenterOfRectangle(Rectangle rectangle)
        {
            return new Point(rectangle.X + rectangle.Width / 2, rectangle.Y + rectangle.Height / 2);
        }
    }
}
