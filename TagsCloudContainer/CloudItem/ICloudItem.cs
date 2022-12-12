using System.Drawing;

namespace TagsCloudContainer
{
    public interface ICloudItem
    {
        public string Word { get; }
        public Rectangle Rectangle { get; }
        public Font Font { get; }
    }
}
