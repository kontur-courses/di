using System.Drawing;
using App.Infrastructure.Words.Tags;

namespace App.Implementation.Words.Tags
{
    public class Tag : ITag
    {
        public Tag(string word, float wordEmSize, Rectangle wordOuterRectangle)
        {
            Word = word;
            WordEmSize = wordEmSize;
            WordOuterRectangle = wordOuterRectangle;
        }

        public static Font WordFont { get; set; }

        public string Word { get; }

        public float WordEmSize { get; }

        public Rectangle WordOuterRectangle { get; set; }
    }
}