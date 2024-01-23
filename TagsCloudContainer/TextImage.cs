using System.Drawing;

namespace TagsCloudContainer
{
    public class TextImage
    {
        public readonly string Text;
        public readonly Size Size;
        public readonly Font Font;

        public TextImage(string text, Font font, Size size)
        {
            Text = text;
            Size = size;
            Font = font;
        }
    }
}
