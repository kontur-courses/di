using System.Drawing;

namespace TagsCloudContainer
{
    public class TextImage
    {
        public readonly string Text;
        public readonly Size Size;
        public readonly Font Font;
        public readonly Color Color;

        public TextImage(string text, Font font, Size size, Color color)
        {
            Text = text;
            Size = size;
            Font = font;
            Color = color;
        }
    }
}
