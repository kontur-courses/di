using System.Drawing;

namespace TagsCloudVisualization
{
    public readonly struct Tag
    {
        public readonly string Word;
        public readonly Rectangle Rectangle;

        public Tag(string word, Rectangle rectangle)
        {
            Word = word;
            Rectangle = rectangle;
        }

        public static Tag FromRectangle(Rectangle rectangle) => new(string.Empty, rectangle);
    }
}