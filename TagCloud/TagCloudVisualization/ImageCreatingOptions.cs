using System.Drawing;

namespace TagCloudVisualization
{
    public class ImageCreatingOptions
    {
        public ImageCreatingOptions(Brush brush, Font font, Point center)
        {
            Brush = brush;
            Font = font;
            Center = center;
        }

        public Point Center { get; }
        public Font Font { get; }
        public Brush Brush { get; }
    }
}
