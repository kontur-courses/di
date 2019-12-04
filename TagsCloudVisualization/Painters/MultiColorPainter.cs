using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class MultiColorPainter : Painter
    {
        private readonly Bitmap field;
        private Brush brush;

        public MultiColorPainter(Size size) : base(size)
        {
            field = new Bitmap(size.Width, size.Height);
        }

        public Bitmap GetMultiColorCloud(IEnumerable<Rectangle> rectangles)
        {
            using (var graphics = Graphics.FromImage(field))
            {
                foreach (var rectangle in rectangles)
                {
                    brush = new SolidBrush(GetRandomColor());
                    graphics.FillRectangle(brush, rectangle);
                }
            }

            return field;
        }

        internal override Bitmap GetImage(IEnumerable<string> words, IEnumerable<Rectangle> rectangles,
            VisualisingOptions visualisingOptions)
        {
            using (var graphics = Graphics.FromImage(field))
            {
                graphics.Clear(visualisingOptions.BackgroundColor);
                var rectangle = rectangles.GetEnumerator();
                foreach (var word in words)
                {
                    rectangle.MoveNext();
                    var color = GetRandomColor();
                    while (color == visualisingOptions.BackgroundColor)
                        color = GetRandomColor();
                    brush = new SolidBrush(color);
                    graphics.DrawString(word, visualisingOptions.Font, brush, rectangle.Current.Location);
                }
            }

            return field;
        }

        private Color GetRandomColor()
        {
            var random = new Random();
            return Color.FromArgb(random.Next(256),
                random.Next(256), random.Next(256));
        }
    }
}