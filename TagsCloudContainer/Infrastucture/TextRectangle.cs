namespace TagsCloudContainer.Infrastucture
{
    public class TextRectangle
    {
        public RectangleF Rectangle { get; }

        public string Text { get; }

        public Font Font { get; }

        public TextRectangle(RectangleF rectangle, string text, Font font)
        {
            Rectangle = rectangle;
            Text = text;
            Font = font;
        }

        public float Area => Rectangle.Width * Rectangle.Height;

    }
}
