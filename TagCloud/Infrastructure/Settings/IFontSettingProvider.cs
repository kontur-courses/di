using System.Drawing;

namespace TagCloud.Infrastructure.Settings
{
    public interface IFontSettingProvider
    {
        public FontFamily FontFamily { get; set; }
        public int MinFontSize { get; }
        public int MaxFontSize { get; }
    }
}