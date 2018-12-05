using System.Drawing;
using Rectangle = TagsCloudVisualization.Layouter.Rectangle;

namespace TagsCloudVisualization
{
    public class PositionedElement
    {
        public string Word { get; }
        public int Frequency { get; }
        public Rectangle Rectangle { get; }
        public Font Font { get; }

        public PositionedElement(FrequentedFontedWord word, Rectangle rectangle)
        {
            Word = word.Word;
            Frequency = word.Frequency;
            Rectangle = rectangle;
            Font = word.Font;
        }
    }
}