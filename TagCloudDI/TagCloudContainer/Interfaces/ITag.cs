using System.Drawing;

namespace TagCloudContainer.Interfaces
{
    public interface ITag
    {
        public string Word { get; }
        public int SizeFont { get; }
        public FontFamily Font { get; }
    }
}
