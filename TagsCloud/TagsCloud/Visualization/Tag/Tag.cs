using System.Drawing;

namespace TagsCloud.Visualization.Tag
{
    public class Tag
    {
        public readonly Rectangle LocationRectangle;

        public readonly string Word;

        public readonly int Size;

        public readonly int Frequency;

        public Tag(Rectangle locationRectangle, string word, int size, int frequency)
        {
            LocationRectangle = locationRectangle;
            Word = word;
            Size = size;
            Frequency = frequency;
        }
    }
}