using System.Drawing;

namespace TagsCloudContainer
{
    public class TextContainer
    {
        public string Text { get; private set; }
        public Rectangle Rectangle { get; private set; }
        public Font Font { get; private set; }

        public TextContainer(string text, Rectangle rectangle, Font font)
        {
            Text = text;
            Rectangle = rectangle;
            Font = font;
        }
    }
}