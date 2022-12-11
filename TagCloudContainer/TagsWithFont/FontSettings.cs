using System.Drawing;

namespace TagCloudContainer.TagsWithFont
{
    public class FontSettings : IFontSettings
    {
        public int MaxFont { get; set; }
        public int MinFont { get; set; }
        public FontFamily Font { get; set; }
    }
}
