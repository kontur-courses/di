using System.Drawing;

namespace TagsCloudContainer.Visualization.Measurers
{
    public class Tag
    {
        internal readonly string Text;
        internal readonly Font Font;
        internal readonly Rectangle Rectangle;

        internal Tag(string text, Font font, Rectangle rectangle)
        {
            Text = text;
            Font = font;
            Rectangle = rectangle;
        }
    }
}