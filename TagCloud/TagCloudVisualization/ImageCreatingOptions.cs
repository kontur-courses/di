using System.Drawing;

namespace TagCloudVisualization
{
    public class ImageCreatingOptions
    {
        public ImageCreatingOptions(Brush brush, string fontName, Point center, Size? imageSize = null)
        {
            Brush = brush;
            FontName = fontName;
            Center = center;
            ImageSize = imageSize;
        }

        public Point Center { get; }
        public string FontName { get; }
        public Brush Brush { get; }
        public Size? ImageSize { get; }
    }
}
