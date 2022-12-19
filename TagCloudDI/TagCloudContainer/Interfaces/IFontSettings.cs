using System.Drawing;

namespace TagCloudContainer.Interfaces
{
    public interface IFontSettings
    {
        public int MaxFont { get; set; }
        public int MinFont { get; set; }
        public FontFamily Font { get; set; }
    }
}
