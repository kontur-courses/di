using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using WordCloudImageGenerator;

namespace WordCloud.CloudControl
{
    public class Vizualizer
    {
        private IEnumerable<Brush> palette;

        public Vizualizer(IEnumerable<Brush> palette)
        {
            this.palette = palette;
        }

        public Bitmap DrawItems(List<CloudItem> items)
        {
            var backgroundSize = GetBackgroundSize(items.Select(item => item.Rectangle).ToList());
            var mainFrame = new Rectangle(new Point(0, 0), backgroundSize);
            var image = new Bitmap(backgroundSize.Width, backgroundSize.Height);
            var graphics = Graphics.FromImage(image);
            var pen = new Pen(Color.Black, 1);
            graphics.FillRectangle(Brushes.Wheat, mainFrame);

            var random = new Random();
            var colorList = palette.ToList();

            foreach (var item in items)
            {
                var numberColor = random.Next(0, palette.Count());

                var brush = colorList[numberColor];
                var rect = item.Rectangle;
                var text = item.Word.Text;
                var font = item.Font;
                graphics.FillRectangle(brush, rect);
                graphics.DrawRectangle(pen, rect);

                Point point = new Point(rect.X, rect.Y);
                graphics.DrawString(text, font, Brushes.Black, point);
            }

            return image;
        }

        private Size GetBackgroundSize(IEnumerable<Rectangle> rectangles)
        {
            var rectangleList = rectangles.ToList();
            var maxX = rectangleList.Select(r => r.Right).Max();
            var maxY = rectangleList.Select(r => r.Bottom).Max();

            var minX = rectangleList.Select(r => r.Left).Min();
            var minY = rectangleList.Select(r => r.Top).Min();

            var width = Math.Sqrt(Math.Pow((maxX - minX), 2));
            var height = Math.Sqrt(Math.Pow((maxY - minY), 2));

            Size sizeBackground = new Size((int) width, (int) height);
            return sizeBackground;
        }
    }
}
