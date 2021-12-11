using System.Drawing;

namespace TagsCloudContainer
{
    public class Tag
    {
        public readonly string Word;
        public readonly Rectangle Rectangle;
        public readonly Font Font;

        public Tag(string word, Rectangle rectangle, Font font)
        {
            Word = word;
            Rectangle = rectangle;
            Font = font;
        }
    }
}