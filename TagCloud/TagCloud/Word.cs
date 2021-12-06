using System.Drawing;

namespace TagCloud
{
    public class Word
    {
        public Font Font { get; set; }
        public Rectangle Rectangle { get; set; }
        public readonly string Text;

        public Word(string text, Font font)
        {
            Text = text;
            Font = font;
        }
        
        public Word(string text, Font font, Rectangle rectangle)
        {
            Text = text;
            Font = font;
            Rectangle = rectangle;
        }
    }
}