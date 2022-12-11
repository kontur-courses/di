using System.Drawing;

namespace TagCloudGraphicalUserInterface
{
    public class TextRectangle
    {
        public Rectangle rectangle { get; }
        public string text { get; }
        public Font font { get; }
        public TextRectangle(Rectangle rectangle, string text, Font font)
        {
            this.rectangle = rectangle;
            this.text = text;
            this.font = font ?? throw new ArgumentNullException(nameof(font));
        }
    }
}