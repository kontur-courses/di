using System.Drawing;

namespace TagsCloudVisualization
{
    public class Tag
    {
        public readonly string Value;
        public readonly Font Font;
        public readonly Color Color;
        public readonly RectangleF Location;

        public Tag(string value, Font font, Color color, RectangleF location)
        {
            Value = value;
            Font = font;
            Color = color;
            Location = location;
        }
    }
}