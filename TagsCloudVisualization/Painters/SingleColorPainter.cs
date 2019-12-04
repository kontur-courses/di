using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class SingleColorPainter : Painter
    {
        private readonly Bitmap field;
        private Brush brush;

        public SingleColorPainter(Size size) : base(size)
        {
            field = new Bitmap(size.Width, size.Height);
        }

        internal override Bitmap GetImage(IEnumerable<string> words, IEnumerable<Rectangle> rectangles,
            VisualisingOptions visualisingOptions)
        {
            using (var graphics = Graphics.FromImage(field))
            {
                graphics.Clear(visualisingOptions.BackgroundColor);
                brush = new SolidBrush(visualisingOptions.TextColor);
                var rectangle = rectangles.GetEnumerator();
                foreach (var word in words)
                {
                    rectangle.MoveNext();
                    graphics.DrawString(word, visualisingOptions.Font, brush, rectangle.Current.Location);
                }
            }

            return field;
        }
    }
}