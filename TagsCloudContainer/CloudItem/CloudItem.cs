using System.Drawing;

namespace TagsCloudContainer
{
    public class CloudItem : ICloudItem
    {
        public string Word { get; }
        public Rectangle Rectangle { get; }
        public Font Font { get; }

        public CloudItem(string word, Rectangle size, Font font)
        {
            Word = word;
            Rectangle = size;
            Font = font;
        }
    }
}
