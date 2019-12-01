using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class SingleColorPainter : Painter
    {
        private readonly Bitmap field;
        private readonly Graphics image;
        private Brush brush;
        
        public SingleColorPainter(Size size) : base(size)
        {
            field = new Bitmap(size.Width, size.Height);
            image = Graphics.FromImage(field);
            image.Clear(Color.White);
        }

        internal override Bitmap GetImage(IEnumerable<string> words, IEnumerable<Rectangle> rectangles, Options options)
        {
            image.Clear(options.BackgroundColor);
            brush = new SolidBrush(options.TextColor);
            var rectangle = rectangles.GetEnumerator();
            foreach (var word in words)
            {
                rectangle.MoveNext();
                image.DrawString(word, options.Font, brush, rectangle.Current.Location);
            }

            return field;
        }
        
        public Bitmap GetSingleColorCloud(Color backgroundColor, Color brushColor, IEnumerable<Rectangle> rectangles)
        {
            image.Clear(backgroundColor);
            brush = new SolidBrush(brushColor);
            foreach (var rectangle in rectangles)
            {
                image.FillRectangle(brush, rectangle);
            }

            return field;
        }
    }
}