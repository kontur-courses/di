using System.Drawing;

namespace TagCloudContainer.Rectangles
{
    public class SizeTextRectangle
    {
        public readonly Size rectangle;
        public readonly string text;
        public readonly Font font;
        public SizeTextRectangle(Size rectangle, string text, Font font)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text));
            this.rectangle = rectangle;
            this.text = text;
            this.font = font ?? throw new ArgumentNullException(nameof(font));
        }
    }
}
