using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class MultiColorPainter : Painter
    {
        private readonly Bitmap field;
        private readonly Graphics image;
        private Brush brush;
        
        public MultiColorPainter(Size size) : base(size)
        {
            field = new Bitmap(size.Width, size.Height);
            image = Graphics.FromImage(field);
            image.Clear(Color.White);
        }
        
        public Bitmap GetMultiColorCloud(IEnumerable<Rectangle> rectangles)
        {
            foreach (var rectangle in rectangles)
            {
                brush = new SolidBrush(GetRandomColor());
                image.FillRectangle(brush, rectangle);
            }

            return field;
        }
        
        internal override Bitmap GetImage(IEnumerable<string> words, IEnumerable<Rectangle> rectangles, Options options)
        {
            image.Clear(options.BackgroundColor);
            var rectangle = rectangles.GetEnumerator();
            foreach (var word in words)
            {
                rectangle.MoveNext();
                var color = GetRandomColor();
                while(color == options.BackgroundColor)
                    color = GetRandomColor();
                brush = new SolidBrush(color);
                image.DrawString(word, options.Font, brush, rectangle.Current.Location);
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