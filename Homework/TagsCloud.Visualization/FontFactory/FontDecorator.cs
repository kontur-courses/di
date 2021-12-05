using System.Drawing;

namespace TagsCloud.Visualization.FontFactory
{
    public class FontDecorator
    {
        private readonly string fontFamily;
        private readonly FontStyle fontStyle;
        private readonly int size;

        public FontDecorator(string fontFamily, int size, FontStyle fontStyle)
        {
            this.fontFamily = fontFamily;
            this.size = size;
            this.fontStyle = fontStyle;
        }

        public Font Build() => new(fontFamily, size, fontStyle);
    }
}