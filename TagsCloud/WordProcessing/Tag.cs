using System.Drawing;

namespace TagsCloud.WordProcessing
{
    public class Tag
    {
        public readonly Font font;
        public readonly Color colorTag;
        public readonly string word;

        public Tag(Font font, Color color, string word)
        {
            this.font = font;
            colorTag = color;
            this.word = word;
        }
    }
}
