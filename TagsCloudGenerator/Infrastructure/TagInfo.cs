using System.Drawing;

namespace TagsCloudGenerator.Infrastructure
{
    public class TagInfo
    {
        public readonly string Value;
        public readonly Font Font;
        public Rectangle Rectangle { get; private set; }

        public TagInfo(string value, Font font, Rectangle rectangle)
        {
            Value = value;
            Font = font;
            Rectangle = rectangle;
        }
    }
}