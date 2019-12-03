using System.Drawing;
namespace TagsCloudVisualization
{
    public class ImageSettings
    {
        public readonly Font Font;
        public readonly Color FontColor;
        public readonly Size ImageSize;
        public readonly Point CloudCenter;

        public ImageSettings(Font font, Color fontColor, Size imageSize, Point cloudCenter)
        {
            Font = font;
            FontColor = fontColor;
            ImageSize = imageSize;
            CloudCenter = cloudCenter;
        }

        public static ImageSettings InitializeDefaultSettings()
        {
            var font = new Font(FontFamily.GenericSansSerif, 30, FontStyle.Bold);
            return new ImageSettings(font , Color.Black, new Size(2000, 2000), new Point(500,500));
        }
    }
}