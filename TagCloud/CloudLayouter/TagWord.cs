using System.Drawing;

namespace TagCloud.CloudLayouter
{
    public class TagWord
    {
        public Rectangle Square { get; }

        public TagWord(Rectangle rect)
        {
            Square = rect;
        }
    }
}