using System.Drawing;

namespace TagsCloudContainer.Core
{
    class Tag
    {
        public string Word { get; }
        public Rectangle Rectangle { get; }
        public int FontSize { get; }

        public Tag(string word, Rectangle rectangle, int fontSize)
        {
            Word = word;
            Rectangle = rectangle;
            FontSize = fontSize;
        }
    }
}