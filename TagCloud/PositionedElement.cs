using System.Drawing;
using Rectangle = TagCloud.Layouter.Rectangle;

namespace TagCloud
{
    public class PositionedElement
    {
        public PositionedElement(FrequentedFontedWord word, Rectangle rectangle)
        {
            Word = word.Word;
            Frequency = word.Frequency;
            Rectangle = rectangle;
            Font = word.Font;
        }

        public string Word { get; }
        public int Frequency { get; }
        public Rectangle Rectangle { get; }
        public Font Font { get; }
    }
}