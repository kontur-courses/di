using System.Drawing;

namespace TagCloudVisualization
{
    public class ImageCreatingOptions
    {
        public ImageCreatingOptions(Brush brush, Font font, Point center, Size? imageSize = null)
        {
            Brush = brush;
            Font = font;
            Center = center;
            ImageSize = imageSize;
        }

        public Point Center { get; }
        public Font Font { get; }
        public Brush Brush { get; }
        public Size? ImageSize { get;  }
    }
}
