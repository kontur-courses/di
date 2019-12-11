using System.Drawing;

namespace TagsCloudGenerator.CloudLayouter
{
    public class Word
    {
        public Word(string value, Rectangle rectangle, int count)
        {
            Value = value;
            Rectangle = rectangle;
            Count = count;
        }

        public string Value { get; }
        public Rectangle Rectangle { get; }
        public int Count { get; }
    }
}