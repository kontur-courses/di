using System.Drawing;

namespace TagsCloudVisualization.Core
{
    public class LayoutedWord : Word
    {
        public readonly Rectangle Position;

        public LayoutedWord(string value, Rectangle position) : base(value)
        {
            Position = position;
        }

        public LayoutedWord(Word word, Rectangle position) : base(word.Value)
        {
            Position = position;
        }
    }
}