using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using TagCloud;
using TagCloud.Extensions;

namespace TagCloud_ConsoleUI
{
    internal class DemoImageGenerator
    {
        /// <summary>
        /// Генерирует облако тэгов с заданным количеством тэгов с рандомными размерами
        /// </summary>
        /// <param name="tagsCount"></param>
        public static void GenerateCircularTagCloud(int tagsCount, ISpiral spiral)
        {
            var rectSizes = new List<Size>();
            var rnd = new Random();
            for (int i = 0; i < tagsCount; i++)
            {
                var width = rnd.Next(14, 60);
                var height = rnd.Next(10, width);
                rectSizes.Add(new Size(width, height));
            }
            GenerateCircularTagCloud(rectSizes, spiral);
        }

        /// <summary>
        /// Генерирует облако тэгов с прямоугольниками заданных размеров
        /// </summary>
        /// <param name="tagsCount"></param>
        /// <param name="rectSizes"></param>
        public static void GenerateCircularTagCloud(IEnumerable<Size> rectSizes, ISpiral spiral)
        {
            var layouter = new CircularCloudLayouter(Point.Empty, spiral);
            rectSizes.ToList().ForEach(s => layouter.PutNextRectangle(s));
            var drawer = new TagCloudDrawer(layouter.Center);
            using (var bitmap = drawer.Draw(layouter.Rectangles))
                bitmap.SaveDefault();
        }

        public static void GenerateSpiral(int count, ISpiral spiral)
        {
            var size = new Size(500, 500);
            var bitmap = new Bitmap(size.Width, size.Height);
            var g = Graphics.FromImage(bitmap);
            g.FillRectangle(Brushes.White, new Rectangle(Point.Empty, bitmap.Size));

            foreach (var p in spiral.GetDiscretePoints().Take(500))
            {
                var point = new Point(p.X + size.Width / 2, p.Y + size.Height / 2);
                g.DrawEllipse(Pens.Red, new Rectangle(point, new Size(1, 1)));
            }
            bitmap.Save($"spiral0{count}.png", ImageFormat.Png);
        }
    }
}
