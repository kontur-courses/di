using System.Drawing;

namespace TagsCloudVisualization
{
    public class ImageSettings
    {
        public static readonly ImageSettings DefaultSettings = InitializeDefaultSettings();
        public readonly Font Font;
        public readonly Size ImageSize;
        public readonly Point CloudCenter;

        public ImageSettings(Font font, Size imageSize, Point cloudCenter)
        {
            Font = font ?? DefaultSettings.Font;
            ImageSize = imageSize;
            CloudCenter = cloudCenter;
        }

        private static ImageSettings InitializeDefaultSettings()
        {
            var font = new Font(FontFamily.GenericSansSerif, 30, FontStyle.Bold);
            return new ImageSettings(font, new Size(2000, 2000), new Point(1000,1000));
        }
    }
}