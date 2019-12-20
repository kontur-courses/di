using System.Drawing;

namespace CloudDrawing
{
    public class ImageSettings
    {
        public ImageSettings(Color background, Size size)
        {
            Background = background;
            Size = size;
        }

        public Color Background { get; }
        public Size Size { get; }
    }
}