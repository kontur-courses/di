using System.Drawing;
namespace TagsCloudVisualization
{
    public class ImageSettings
    {
        public readonly Font Font;
        public readonly ITagPainter Painter;
        public readonly Size ImageSize;
        public readonly Point CloudCenter;

        public ImageSettings(Font font, ITagPainter painter, Size imageSize, Point cloudCenter)
        {
            Font = font;
            Painter = painter;
            ImageSize = imageSize;
            CloudCenter = cloudCenter;
        }

        public static ImageSettings InitializeDefaultSettings()
        {
            var font = new Font(FontFamily.GenericSansSerif, 30, FontStyle.Bold);
            return new ImageSettings(font , new RandomTagPainter(), new Size(2000, 2000), new Point(500,500));
        }
    }
}