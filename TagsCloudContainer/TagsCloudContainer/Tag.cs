using System.Drawing;

namespace TagsCloudContainer
{
    public class Tag
    {
        public readonly string Word;
        public readonly Size Size;

        public Tag(string word, Size size)
        {
            Word = word;
            Size = size;
        }
    }
}