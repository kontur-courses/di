using System.Drawing;
using System.Drawing.Text;

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

        /*
        public void ChangeSizeOfField(Font font)
        {
            //var stringSize = visualization.GetStringSize(this);
            var stringSize = GetStringSize(font);

            var newSize = new Size(stringSize.Width, stringSize.Height);
            RectangleElement = new Rectangle(RectangleElement.Location, newSize);
        }
        */

        public static RectangleWithWord MakeFakeWordRectangle(Size rectangleSize)
        {
            var rectangle = new Rectangle(new Point(0, 0), rectangleSize);
            return new RectangleWithWord(rectangle, new Word(""));
        }

        /*
        private Size GetStringSize(Font font)
        {


            var stringSize = graphics.MeasureString(WordElement.WordText, font);

            var rectSize = new Size((int)stringSize.Width, (int)stringSize.Height);

            return rectSize;

        }
        */
        /*
            var image = new Bitmap(ImageSize.Width, ImageSize.Height);
            using (var graphics = Graphics.FromImage(image))
            {
                var fontSize = TextFont.Size + 3 * word.WordElement.CntOfWords;
                var font = new Font("Times", fontSize);

                var stringSize = graphics.MeasureString(word.WordElement.WordText, font);

                var rectSize = new Size((int)stringSize.Width, (int)stringSize.Height);

                return rectSize;
            }
            */
    }
}
