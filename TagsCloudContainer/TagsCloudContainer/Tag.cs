using System.Drawing;

namespace TagsCloudContainer.TagsCloudContainer
{
    public class Tag
    {
        public Tag(string text, Rectangle rectangle, Font font)
        {
            Text = text;
            Rectangle = rectangle;
            Font = font;
        }

        public string Text { get; }
        public Rectangle Rectangle { get; }
        public Font Font { get; }


    }
}