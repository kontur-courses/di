using System.Drawing;

namespace TagsCloudVisualization
{
    public class Tag
    {
        public Tag(string word, Rectangle rectangle)
        {
            Word = word;
            Rectangle = rectangle;
        }

        public string Word { get; }
        public Rectangle Rectangle { get; }

        public static Tag FromRectangle(Rectangle rectangle) => new(string.Empty, rectangle);
    }
}