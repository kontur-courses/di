using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public static class Drawer
    {
        private static Bitmap _bitmap;

        public static void DrawRectangles(Point center, IEnumerable<Rectangle> rectangles)
        {
            _bitmap = new Bitmap(800, 800);
            var graph = Graphics.FromImage(_bitmap);
            graph.TranslateTransform(center.X, center.Y);

            var random = new Random();
            foreach (var rectangle in rectangles)
            {
                var pen = new Pen(GetRandomColor(random), 2);
                graph.DrawRectangle(pen, rectangle);
            }
        }

        public static void SaveImage(string path)
        {
            _bitmap.Save(path);
        }

        private static Color GetRandomColor(Random random)
        {
            return Color.FromArgb(
                random.Next(256),
                random.Next(256),
                random.Next(256));
        }
    }
}