using System.Drawing;

namespace TagCloudContainer.TagsWithFont
{
    public interface ITag
    {
        public string Word { get; }
        public int SizeFont { get; }
        public FontFamily Font { get; }
    }
}
