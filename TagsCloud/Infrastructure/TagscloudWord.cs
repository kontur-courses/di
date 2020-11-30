using System.Drawing;

namespace TagsCloud.Infrastructure
{
    class TagscloudWord
    {
        public readonly string Value;
        public readonly Font Font;
        public Point Position;

        public TagscloudWord(string value, Font font, Point position)
        {
            Value = value;
            Font = font;
            Position = position;
        }
    }
}
