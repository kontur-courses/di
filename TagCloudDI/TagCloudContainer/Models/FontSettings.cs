using System.Drawing;
using TagCloudContainer.Interfaces;

namespace TagCloudContainer.Models
{
    public class FontSettings : IFontSettings
    {
        public int MaxFont { get; set; }
        public int MinFont { get; set; }
        public FontFamily Font { get; set; }
    }
}
