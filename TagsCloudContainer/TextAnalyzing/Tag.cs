using System.Drawing;

namespace TagsCloudContainer.TextAnalyzing
{
    public class Tag
    {
        public readonly int FontSize;
        public readonly Rectangle Rectangle;
        public readonly string Text;

        public Tag(Rectangle rectangle, string text, int fontSize)
        {
            Rectangle = rectangle;
            Text = text;
            FontSize = fontSize;
        }
    }
}