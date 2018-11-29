using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
    class TagCloudVisualization
    {
        public TagCloudVisualization(ICloudLayouter cloudLayouter)
        {
            this.cloudLayouter = cloudLayouter;

            bitmapHeight = cloudLayouter.Spiral.Center.Y * 2;
            bitmapWidth = cloudLayouter.Spiral.Center.X * 2;
        }

        private static void FillCloudWithRectangles(ICloudLayouter cloud, int count, int minSize, int maxSize)
        {
            var rnd = new Random();
            for (var i = 0; i < count; i++)
            {
                var width = rnd.Next(minSize * 10, maxSize * 10);
                var height = rnd.Next(minSize, maxSize);

                var size = new Size(width, height);

                cloud.PutNextRectangle(size);
            }
        }

        private readonly ICloudLayouter cloudLayouter;

        private readonly int bitmapWidth;
        private readonly int bitmapHeight;

        public void SaveCloudLayouter(string bitmapName, string directory, int count, int minSize, int maxSize)
        {
            FillCloudWithRectangles(cloudLayouter, count, minSize, maxSize);
            var countOfRectangles = cloudLayouter.Rectangles.Count;
            var bitmap = GetBitmapWithRectangles();
            var path = $"{directory}\\..\\..\\Images\\{bitmapName}-{countOfRectangles}.png";

            bitmap.Save(path, ImageFormat.Png);
        }

        public void SaveCloudLayouter(string bitmapName, string directory, Font font, IEnumerable<string> words)
        {
            var bitmap = new Bitmap(bitmapWidth, bitmapHeight);
            var g = Graphics.FromImage(bitmap);

            var i = 1;

            var count = words.Count();
            var delta = font.Size / 2 / count;

            foreach (var word in words)
            {
                font = new Font(font.Name, font.Size - delta);
                var brush = new SolidBrush(GetColorOfWord(i, count + 1));
                var size = GetSizeOfWord(word, font);
                var rec = cloudLayouter.PutNextRectangle(size);
                g.DrawString(word, font, brush, rec);
                i++;
            }

            var countOfRectangles = cloudLayouter.Rectangles.Count;
            var path = $"{directory}\\..\\..\\Images\\{bitmapName}-{countOfRectangles}.png";

            bitmap.Save(path, ImageFormat.Png);
        }

        private Size GetSizeOfWord(string word, Font font)
        {
            return new Size((int)(font.Size * word.Length), (int)(font.Height * 2));
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

        private Color GetColorOfWord(int num, int count)
        {
            var r = (int)(((double)num / count) * 255 * 0.9);
            var g = (int)(((double)num / count) * 255 * 0.7);
            var b = (int)(((double)num / count) * 255 * 0.5);

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
