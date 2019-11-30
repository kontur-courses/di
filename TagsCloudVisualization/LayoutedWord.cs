using System.Drawing;

namespace TagsCloudVisualization
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