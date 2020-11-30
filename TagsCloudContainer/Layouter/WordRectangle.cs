using System.Drawing;

namespace TagsCloudContainer.Layouter
{
    public class WordRectangle
    {
        public Rectangle Rectangle { get; }
        public string Text { get; }
        public int FontSize { get; }

        public WordRectangle(Rectangle rectangle, string text, int fontSize)
        {
            Rectangle = rectangle;
            Text = text;
            FontSize = fontSize;
        }
    }
}