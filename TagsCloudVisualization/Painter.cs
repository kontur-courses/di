using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public abstract class Painter
    {
        private readonly Bitmap field;
        private readonly Graphics image;

        internal Painter(Size size)
        {
            field = new Bitmap(size.Width, size.Height);
            image = Graphics.FromImage(field);
            image.Clear(Color.White);
        }

        internal abstract Bitmap GetImage(IEnumerable<string> words, IEnumerable<Rectangle> rectangles, Options options);
    }
}