using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace TagsCloudVisualization
{
    class CircularCloudVisualization
    {
        private const int CountOfRectangles = 100;
        
        private const int MinSizeOfRectangle = 50;
        private const int MaxSizeOfRectangle = 100;

        private const int BitmapWidth = 5000;
        private const int BitmapHeight = 5000;
        private const string BitmapName = "CircularCloud";

        public static void Main()
        {
            var center = new Point(BitmapWidth / 2, BitmapHeight / 2);
            var spiral = new ArchimedesSpiral(center);

            var cloudLayouter = new CloudLayouter(spiral, center);

            FillCloudWithRectangles(cloudLayouter);

            var bitmap = GetBitmapWithRectangles(cloudLayouter);
            var directory = Environment.CurrentDirectory;

            bitmap.Save($"{directory}\\..\\..\\Images\\{BitmapName}{CountOfRectangles}.png", ImageFormat.Png);
        }


        public static Bitmap GetBitmapWithRectangles(CloudLayouter cloudLayouter)
        {
            var bitmap = new Bitmap(BitmapWidth, BitmapHeight);
            var pen = new Pen(Color.Black);

            var rectangles = cloudLayouter.Rectangles;
            var center = cloudLayouter.Center;

            var maxDist = (int)rectangles
                .Select(x => GetDistanceFromRectangleToPoint(x, center))
                .Max();

            foreach (var rectangle in rectangles)
            {
                var color = GetColorOfRectangle(rectangle, center, maxDist);
                var brush = new SolidBrush(color);

                Graphics.FromImage(bitmap).FillRectangle(brush, rectangle);
                Graphics.FromImage(bitmap).DrawRectangle(pen, rectangle);
            }

            return bitmap;
        }

        private static Color GetColorOfRectangle(Rectangle rectangle, Point center, int maxDist)
        {
            var dist = GetDistanceFromRectangleToPoint(rectangle, center);
            var r = (int)(dist / maxDist * 255 * 0.9);
            var g = (int)(dist / maxDist * 255 * 0.7);
            var b = (int)(dist / maxDist * 255 * 0.5);

            return Color.FromArgb(r, g, b);
        }

        private static void FillCloudWithRectangles(CloudLayouter cloud)
        {
            var rnd = new Random();

            for (var i = 0; i < CountOfRectangles; i++)
            {
                var width = rnd.Next(MinSizeOfRectangle * 10, MaxSizeOfRectangle * 10);
                var height = rnd.Next(MinSizeOfRectangle, MaxSizeOfRectangle);

                var size = new Size(width, height);

                cloud.PutNextRectangle(size);
            }
        }

        private static double GetDistanceFromRectangleToPoint(Rectangle rectangle, Point center)
        {
            return Math.Sqrt((GetCenterOfRectangle(rectangle).X - center.X) *
                             (GetCenterOfRectangle(rectangle).X - center.X) +
                             (GetCenterOfRectangle(rectangle).Y - center.Y) *
                             (GetCenterOfRectangle(rectangle).Y - center.Y));
        }

        private static Point GetCenterOfRectangle(Rectangle rectangle)
        {
            return new Point(rectangle.X + rectangle.Width / 2, rectangle.Y + rectangle.Height / 2);
        }
    }
}
