using System.Drawing;

namespace TagsCloudVisualization
{
    public class RectangleWithWord
    {
        public Word WordElement { get; set; }
        public Rectangle RectangleElement { get; set; }

        public RectangleWithWord(Rectangle rectangle, Word word)
        {
            WordElement = word;
            RectangleElement = rectangle;
        }
    }
}
