using System.Drawing;

namespace TagsCloud.Infrastructure
{
    public class Tag
    {
        public readonly string Value;
        public readonly Font Font;
        public Rectangle Rectangle;

        public Tag(string value, Font font, Rectangle rectangle)
        {
            Value = value;
            Font = font;
            Rectangle = rectangle;
        }

        public Tag RescaleTag(float tagsCloudSizeRatio)
        {
            var tagBounds = new Rectangle((int)(Rectangle.X * tagsCloudSizeRatio),
                (int)(Rectangle.Y * tagsCloudSizeRatio),
                (int)(Rectangle.Width * tagsCloudSizeRatio),
                (int)(Rectangle.Height * tagsCloudSizeRatio));
            var tagFontSize = (int) (Font.Size * tagsCloudSizeRatio);
            if (tagFontSize == 0)
                tagFontSize = 1;
            return new Tag(Value,
                new Font(Font.FontFamily,
                    tagFontSize, Font.Style), tagBounds);
        }
    }
}
