using System.Drawing;

namespace TagsCloudContainer.Visualization
{
    public class TagsCloudSetting : ICloudSetting
    {
        public Font Font { get; }
        public Size ImageSize { get; }
        public Color TextColor { get; }
        public Color BackgroundColor { get; }
        public Point GetCenterImage()
        {
            return new Point(ImageSize.Width / 2, ImageSize.Height / 2);
        }

        public TagsCloudSetting(Font font, Size imageSize, Color textColor, Color backgroundColor)
        {
            Font = font;
            ImageSize = imageSize;
            TextColor = textColor;
            BackgroundColor = backgroundColor;
        }

        public static TagsCloudSetting GetDefault()
        {
            return new TagsCloudSetting(new Font(FontFamily.GenericMonospace,60), new Size(1000,1000), Color.Red,Color.Black);
        }
    }
}