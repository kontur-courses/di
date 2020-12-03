using System.Drawing;

namespace TagsCloudContainer.TagsCloudContainer.Interfaces
{
    public interface ITag
    {
        public string Text { get; }
        public Size Size { get; }
        public Rectangle Rectangle { get; }
        public Font Font { get; }
    }
}