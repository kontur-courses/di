using System.Drawing;

namespace TagsCloudContainer.CloudItem
{
    public class TagCloudItem : ICloudItem
    {
        public TagCloudItem(string word, Rectangle size, Font font)
        {
            Word = word;
            Rectangle = size;
            Font = font;
        }

        public string Word { get; }
        public Rectangle Rectangle { get; }
        public Font Font { get; }
    }
}