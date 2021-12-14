using System.Drawing;

namespace TagCloud.Templates
{
    public class WordParameter
    {
        public RectangleF WordRectangleF { get; set; }
        public string Word { get; }
        public Color Color { get; }
        public Font Font { get; }

        public WordParameter(string word, Font font, Color color)
        {
            Font = font;
            Color = color;
            Word = word;
        }
    }
}