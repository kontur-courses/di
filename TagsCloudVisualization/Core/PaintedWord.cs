using System.Drawing;

namespace TagsCloudVisualization.Core
{
    public class PaintedWord : LayoutedWord
    {
        public readonly Color FontColor;

        public PaintedWord(string value, Rectangle position, Color color) : base(value, position)
        {
            FontColor = color;
        }

        public PaintedWord(LayoutedWord word, Color color) : base(word.Value, word.Position)
        {
            FontColor = color;
        }
    }
}