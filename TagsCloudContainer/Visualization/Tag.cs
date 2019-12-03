using System.Drawing;

namespace TagsCloudContainer.Visualization
{
    public class Tag
    {
        public readonly string Text;
        public readonly Font Font;
        public readonly Size Size;

        public Tag(string text, Font font, Size size)
        {
            Text = text;
            Font = font;
            Size = size;
        }
    }
}