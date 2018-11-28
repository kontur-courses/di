using System.Drawing;

namespace TagCloud.Settings
{
    public class FontSettings
    {
        public FontFamily FontFamily { get; set; } = new FontFamily("Times New Roman");
        public int MinFontSize { get; set; } = 10;
        public int MaxFontSize { get; set; } = 50;
    }
}