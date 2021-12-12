using System.Drawing;

namespace TagCloud.TextProcessing
{
    public class Word
    {
        public Font Font { get; }
        public Rectangle Rectangle { get; set; }
        public readonly string Text;

        public Word(string text, Font font)
        {
            Text = text;
            Font = font;
        }

        public Word WithFontSize(double fontSize) => new(Text, new Font(Font.FontFamily, (float)fontSize));
    }
}