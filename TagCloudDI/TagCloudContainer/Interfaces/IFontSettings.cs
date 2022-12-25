using System.Drawing;

namespace TagCloudContainer.Interfaces
{
    public interface IFontSettings
    {
        public int MaxFontSize { get; set; }
        public int MinFontSize { get; set; }
        public FontFamily Font { get; set; }
    }
}
