using System.Drawing;

namespace TagsCloud
{
    public class Tag
    {
        public Tag(string word, Rectangle wordBox)
        {
            Word = word;
            WordBox = wordBox;
        }

        public string Word { get; }
        public Rectangle WordBox { get; }
    }
}