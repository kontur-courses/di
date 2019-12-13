using System.Drawing;

namespace TagsCloudContainer.TagCloudVisualization
{
    public class TagCloudItem
    {
        public Rectangle Rectangle { get; }
        public string Word { get; }
        public int Coefficient { get; }

        public TagCloudItem(Rectangle rectangle, string word, int coefficient)
        {
            Rectangle = rectangle;
            Word = word;
            Coefficient = coefficient;
        }
    }
}