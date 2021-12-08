using System;
using System.Drawing;
using TagsCloudVisualizationDI.TextAnalization;

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
            var word = new Word(String.Empty,PartsOfSpeech.SpeechPart.S);
            var fakeLocation = Point.Empty;
            var rectangle = new Rectangle(fakeLocation, size);

            return new RectangleWithWord(rectangle, word);
        }

        public void ChangeSizeOfField()
        {
            var newSize = new Size(RectangleElement.Width  + 1 * WordElement.CntOfWords, 
                RectangleElement.Height + 1 * WordElement.CntOfWords);
            RectangleElement = new Rectangle(RectangleElement.Location, newSize);
        }
    }
}
