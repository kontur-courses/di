using System.Drawing;

namespace TagCloudContainer.Models
{
    public class RectangleWithText
    {
        public Rectangle rectangle { get; }
        public string text { get; }
        public Font font { get; }
        public RectangleWithText(Rectangle rectangle, string text, Font font)
        {
            this.rectangle = rectangle;
            this.text = text;
            this.font = font ?? throw new ArgumentNullException(nameof(font));
        }
    }
}