using System.Drawing;

namespace TagCloud.TextProcessing
{
    public class Word
    {
        public readonly string Text;

        public Word(string text, Font font)
        {
            Text = text;
            Font = font;
        }

        public Font Font { get; }
        public Rectangle Rectangle { get; set; }

        public Word WithFontSize(double fontSize)
        {
            return new Word(Text, new Font(Font.FontFamily, (float) fontSize));
        }
    }
}