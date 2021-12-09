using System.Drawing;

namespace TagCloud.Words.Tagging
{
    public class Tag : ITag
    {
        public Tag(string word, float wordKegelSize, Size wordRectangleSize)
        {
            Word = word;
            KegelSize = wordKegelSize;
            WordRectangleSize = wordRectangleSize;
        }

        public string Word { get; }

        public float KegelSize { get; }

        public Size WordRectangleSize { get; }
    }
}