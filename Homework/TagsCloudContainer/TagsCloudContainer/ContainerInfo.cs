using System.Drawing;

namespace TagsCloudContainer
{
    public class ContainerInfo
    {
        public readonly string Text;
        public readonly Color TextColor;
        public readonly Font TextFont;
        public readonly Rectangle Rectangle;

        public ContainerInfo(string text, Color textColor, Font textFont, Rectangle rectangle)
        {
            Text = text;
            TextColor = textColor;
            Rectangle = rectangle;
            TextFont = textFont;
        }

        public ContainerInfo(string text, Rectangle rectangle, Font textFont)
        {
            Text = text;
            TextColor = Color.Black;
            Rectangle = rectangle;
            TextFont = textFont;
        }
    }
}
