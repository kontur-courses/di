using System.Drawing;

namespace TagsCloudContainer.CloudItem
{
    public interface ICloudItem
    {
        public string Word { get; }
        public Rectangle Rectangle { get; }
        public Font Font { get; }
    }
}