using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace TagsCloudVisualization
{
    class Program
    {
        private static readonly Random Random = new Random();
        static void Main(string[] args)
        {
            DrawExample(RectFuncGenerator(300, 10, 50, 10, 50), "example1");
            DrawExample(FillGraduallyDecreasingRectFunc, "example2");
            DrawExample(RectFuncGenerator(100, 5, 60, 5, 60), "example3");
        }

        public static List<Rectangle> FillGraduallyDecreasingRectFunc(CircularCloudLayouter layouter)
        {
            var width = 60;
            const int height = 30;
            var rectangles = new List<Rectangle>();
            for (var i = 0; i < 500; i++)
            {
                if (i % 100 == 0)
                    width -= 5;
                var size = new Size(width, height);
                rectangles.Add(layouter.PutNextRectangle(size));
            }

            return rectangles;
        }

        public static Func<CircularCloudLayouter, List<Rectangle>> RectFuncGenerator(int n, int minWidth, int maxWidth, int minHeight, int maxHeight)
        {
            return layouter =>
            {
                var rectangles = new List<Rectangle>();
                for (var i = 0; i < n; i++)
                {
                    var size = new Size(Random.Next(minWidth, maxWidth), Random.Next(minHeight, maxHeight));
                    rectangles.Add(layouter.PutNextRectangle(size));
                }

                return rectangles;
            };
        }

        public static void DrawExample(Func<CircularCloudLayouter, List<Rectangle>> fillFunc, string nameExample)
        {
            var sizeWindow = new Size(1200, 1200);
            var visualizer = new Visualizer(sizeWindow);
            var layouter = new CircularCloudLayouter(new Point(sizeWindow.Width / 2, sizeWindow.Height / 2));

            var rectangles = fillFunc(layouter);

            visualizer.DrawRectangles(rectangles);
            visualizer.Save(Path.Combine(Directory.GetCurrentDirectory(), $"{nameExample}.bmp"));
        }
    }
}

