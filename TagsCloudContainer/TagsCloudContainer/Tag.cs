using System.Drawing;

namespace TagsCloudContainer.TagsCloudContainer
{
    public class Tag
    {
        public Tag(string text, Rectangle rectangle, Size size, Font font)
        {
            Text = text;
            Rectangle = rectangle;
            Size = size;
            Font = font;
        }

        public string Text { get; }
        public Size Size { get; }
        public Rectangle Rectangle { get; }
        public Font Font { get; }
    }
}