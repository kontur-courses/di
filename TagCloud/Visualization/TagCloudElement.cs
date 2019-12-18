using System.Drawing;
using TagCloud.Infrastructure;

namespace TagCloud.Visualization
{
    public class TagCloudElement
    {
        public Rectangle Rectangle { get; }
        public Color Color { get; }
        public string WordValue => word.Value;
        public Font Font => new Font(fontFamily, word.FontSize);

        private readonly Word word;
        private readonly FontFamily fontFamily;

        public TagCloudElement(Word word, Rectangle rectangle, Color color, FontFamily fontFamily)
        {
            this.word = word;
            Rectangle = rectangle;
            Color = color;
            this.fontFamily = fontFamily;
        }
    }
}
