using System;
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

        public static RectangleWithWord MakeFakeWordRectangle(Size size)
        {
            var word = new Word(String.Empty);
            var fakeLocation = Point.Empty;
            var rectangle = new Rectangle(fakeLocation, size);

            return new RectangleWithWord(rectangle, word);
        }
    }
}
