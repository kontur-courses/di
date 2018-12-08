using TagCloud.Layouter;

namespace TagCloud.IntermediateClasses
{
    public class PositionedElement
    {
        public PositionedElement(FrequentedWord word, Rectangle rectangle)
        {
            Word = word.Word;
            Frequency = word.Frequency;
            Rectangle = rectangle;
        }

        public string Word { get; }
        public int Frequency { get; }
        public Rectangle Rectangle { get; }
    }
}