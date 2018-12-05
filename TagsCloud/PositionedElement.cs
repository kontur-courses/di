using TagsCloudVisualization.Layouter;

namespace TagsCloudVisualization
{
    public class PositionedElement
    {
        public string Word { get; }
        public int Frequency { get; }
        public Rectangle Rectangle { get; }

        public PositionedElement(FrequentedWord word, Rectangle rectangle)
        {
            Word = word.Word;
            Frequency = word.Frequency;
            Rectangle = rectangle;
        }
    }
}