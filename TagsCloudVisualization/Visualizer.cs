using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class Visualizer
    {
        private readonly Point center;
        private readonly int pixelsPerPoint;

        public Visualizer(Point center, int pixelsPerPoint)
        {
            this.center = center;
            this.pixelsPerPoint = pixelsPerPoint;
        }
        
        public Bitmap GetVisualization(List<Rectangle> rectangles)
        {
            var sizeOfImagePayload = GetSizeOfImagePayload(rectangles);
            var sizeOfImage = GetSizeOfResultImage(sizeOfImagePayload);
            var bitmap = new Bitmap(sizeOfImage.Width, sizeOfImage.Height);
            var g = Graphics.FromImage(bitmap);

            var shift = ShiftUtils.GetShiftToTheFirstQuadrant(center, rectangles);
            var shiftedCenter = ShiftUtils.ShiftPoint(center, shift, pixelsPerPoint);
            var resizedAndMovedRectangles = rectangles
                .Select(r =>
                    ShiftUtils.GetShiftedAndResizedRectangle(r, shift, pixelsPerPoint))
                .ToList();
            DrawPicture(g, shiftedCenter, resizedAndMovedRectangles);

            return bitmap;
        }

        private void DrawPicture(Graphics g, Point shiftedCenter, List<Rectangle> rectangles)
        {
            var brushes = new[]
            {
                Brushes.OrangeRed,
                Brushes.Crimson,
                Brushes.Chartreuse
            };

            for (var i = 0; i < rectangles.Count; i++)
            {
                var brush = brushes[(i + 1) % brushes.Length];
                var rectangle = rectangles[i];
                g.FillRectangle(brush, rectangle);
                g.DrawRectangle(new Pen(Color.Black, 2), rectangle);
            }

            g.FillEllipse(Brushes.Yellow, shiftedCenter.X - 4, shiftedCenter.Y - 4, 8, 8);
        }

        public static Size GetSizeOfImagePayload(List<Rectangle> rectangles)
        {
            var left = rectangles.Select(r => r.Left).Min();
            var right = rectangles.Select(r => r.Right).Max();
            var width = right - left;

            var top = rectangles.Select(r => r.Top).Min();
            var bottom = rectangles.Select(r => r.Bottom).Max();
            var height = bottom - top;

            return new Size(width, height);
        }

        private Size GetSizeOfResultImage(Size sizeOfImagePayload)
        {
            var width = sizeOfImagePayload.Width * pixelsPerPoint + 1;
            var height = sizeOfImagePayload.Height * pixelsPerPoint + 1;
            return new Size(width, height);
        }

        public static void DrawVisualization(int rectanglesCount, bool random = true, Size size = new Size())
        {
            var sizes = GetSizes(rectanglesCount, random, size);
            var center = new Point(0, 0);
            var layouter = new CircularCloudLayouter(center);
            var rectangles = sizes.Select(s => layouter.PutNextRectangle(s)).ToList();
            var visualizer = new Visualizer(center, 10);
            var bitmap = visualizer.GetVisualization(rectangles);
            bitmap.Save($"{AppDomain.CurrentDomain.BaseDirectory}\\pic.bmp");
        }

        private static List<Size> GetSizes(int rectanglesCount, bool random = true, Size size = new Size())
        {
            var sizes = new List<Size>();

            if (!random) return Enumerable.Range(0, rectanglesCount).Select(n => size).ToList();
            var rnd = new Random();
            for (var i = 0; i < rectanglesCount; i++)
            {
                var height = rnd.Next(4);
                var width = rnd.Next(4, 10);
                sizes.Add(new Size(width, height));
            }
            return sizes;
        }
    }
}
