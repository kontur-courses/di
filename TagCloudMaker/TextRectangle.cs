using System.Drawing;

namespace TagCloud
{
    public class TextRectangle
    {
        private Rectangle rectangle;

        public Rectangle Rectangle => rectangle;

        public string Text { get; }

        public TextRectangle(Point location, Size size, string text)
        {
            rectangle = new Rectangle(location, size);
            Text = text;
        }

        public void SetLocation(Point newLocation) => rectangle.Location = newLocation;
    }
}