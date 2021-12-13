using System.Drawing;

namespace TagsCloudVisualizationDI
{
    public class RectangleWithWord
    {
        public Word WordElement { get; }
        public Rectangle RectangleElement { get; set; }

        public RectangleWithWord(Rectangle rectangle, Word word)
        {
            WordElement = word;
            RectangleElement = rectangle;
        }


        public static RectangleWithWord MakeFakeWordRectangle(Size rectangleSize)
        {
            var rectangle = new Rectangle(new Point(0, 0), rectangleSize);
            return new RectangleWithWord(rectangle, new Word(""));
        }
    }
}
