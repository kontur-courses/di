using System.Drawing;
using TagsCloudVisualizationDI.TextAnalization.Visualization;

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

        public void ChangeSizeOfField(IVisualization visualization)
        {
            var stringSize = visualization.GetStringSize(this);
            var newSize = new Size(stringSize.Width, stringSize.Height);
            RectangleElement = new Rectangle(RectangleElement.Location, newSize);
        }

        public static RectangleWithWord MakeFakeWordRectangle(Size rectangleSize)
        {
            var rectangle = new Rectangle(new Point(0, 0), rectangleSize);
            return new RectangleWithWord(rectangle, new Word(""));
        }
    }
}
