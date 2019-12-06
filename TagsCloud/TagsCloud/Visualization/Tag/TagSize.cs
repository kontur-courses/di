using System.Drawing;

namespace TagsCloud.Visualization.Tag
{
    public class TagSize
    {
        public readonly Size RectangleSize;
        public readonly int FontSize;

        public TagSize(Size rectangleSize, int fontSize)
        {
            RectangleSize = rectangleSize;
            FontSize = fontSize;
        }
    }
}