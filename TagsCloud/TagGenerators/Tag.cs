using System.Drawing;
using TagsCloud.FontGenerators;

namespace TagsCloud.TagGenerators
{
    public class Tag
    {
        public readonly FontSettings font;
        public readonly Color colorTag;
        public readonly string word;

        public Tag(FontSettings font, Color color, string word)
        {
            this.font = font;
            colorTag = color;
            this.word = word;
        }
    }
}
