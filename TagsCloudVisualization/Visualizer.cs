using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    class Visualizer
    {
        private readonly Bitmap bitmap;
        private readonly Graphics graphics;
        private readonly Pen pen;
        private readonly List<Brush> colors = new List<Brush>() { Brushes.Blue, Brushes.Green, Brushes.Gray,
            Brushes.DeepPink, Brushes.DarkCyan};
        public Visualizer(Size size)
        {
            bitmap = new Bitmap(size.Width, size.Height);
            graphics = Graphics.FromImage(bitmap);
            pen = new Pen(Color.White, 2);
        }

        public void DrawRectangles(IEnumerable<Rectangle> rectangles)
        {
            var random = new Random();
            foreach (var rectangle in rectangles)
            {
                graphics.FillRectangle(colors[random.Next(colors.Count)], rectangle);
                graphics.DrawRectangle(pen, rectangle);
            }
        }

        public void Save(string path)
        {
            bitmap.Save(path);
        }
    }
}
