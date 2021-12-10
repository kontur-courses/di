using System.Drawing;

namespace TagCloud.Words.Tags
{
    public class Tag : ITag
    {
        public Tag(string word, float wordEmSize, Rectangle wordOuterRectangle)
        {
            Word = word;
            WordEmSize = wordEmSize;
            WordOuterRectangle = wordOuterRectangle;
        }

        public static string WordFontName { get; set; }

        public string Word { get; }

        public float WordEmSize { get; }

        public Rectangle WordOuterRectangle { get; set; }
    }
}