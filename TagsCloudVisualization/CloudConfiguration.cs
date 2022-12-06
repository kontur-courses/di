using System.Drawing;

namespace TagsCloudVisualization
{
    public class CloudConfiguration
    {
        public static CloudConfiguration Default => new CloudConfiguration(
            new Size(1500, 1500),
            Color.FromArgb(255, 0, 34, 43),
            Color.FromArgb(255, 217,92,6),
            new FontFamily("Arial")
            );
        
        public Size ImageSize { get; }
        public Color BackgroundColor { get; }
        public Color PrimaryColor { get; }
        public FontFamily FontFamily { get; }
        
        public CloudConfiguration(Size imageSize, Color backgroundColor, Color primaryColor, FontFamily fontFamily)
        {
            ImageSize = imageSize;
            BackgroundColor = backgroundColor;
            PrimaryColor = primaryColor;
            FontFamily = fontFamily;
        }
    }
}