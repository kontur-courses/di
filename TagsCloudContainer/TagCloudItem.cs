using System.Drawing;

namespace TagsCloudContainer
{
    public class TagCloudItem
    {
        public Rectangle Rectangle { get; }
        public string Word { get; }

        public TagCloudItem(Rectangle rectangle, string word)
        {
            Rectangle = rectangle;
            Word = word;
        }
    }
}