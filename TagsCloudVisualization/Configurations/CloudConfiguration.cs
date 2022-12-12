using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudVisualization.Configurations
{
    public class CloudConfiguration : IDisposable
    {
        public static CloudConfiguration Default => new CloudConfiguration(
            new Point(750, 750),
            new Size(1500, 1500),
            Color.FromArgb(255, 0, 34, 43),
            Color.FromArgb(255, 217,92,6),
            new FontFamily("Arial")
        );
        
        public Point Center { get; }
        public Size ImageSize { get; }
        public Color BackgroundColor { get; }
        public Color PrimaryColor { get; }
        public FontFamily FontFamily { get; }

        public CloudConfiguration(Point center, Size imageSize, Color backgroundColor, Color primaryColor, FontFamily fontFamily)
        {
            ImageSize = imageSize;
            BackgroundColor = backgroundColor;
            PrimaryColor = primaryColor;
            FontFamily = fontFamily;
            Center = center;
        }

        public void Dispose()
        {
            FontFamily?.Dispose();
        }
    }
}