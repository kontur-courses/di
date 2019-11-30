using System.Drawing;

namespace TagsCloudVisualization.Core
{
    public class LayoutedWord
    {
        public readonly Word Word;
        public readonly Rectangle Rectangle;

        public LayoutedWord(Word word, Rectangle rectangle)
        {
            Word = word;
            Rectangle = rectangle;
        }
    }
}