using System.Drawing;

namespace TagCloudContainer.Models
{
    public class RectangleWithText
    {
        public Rectangle Rectangle { get; }
        public string Text { get; }
        public Font Font { get; }
        public RectangleWithText(Rectangle rectangle, string text, Font font)
        {
            Rectangle = rectangle;
            Text = text;
            Font = font ?? throw new ArgumentNullException(nameof(font));
        }
    }
}