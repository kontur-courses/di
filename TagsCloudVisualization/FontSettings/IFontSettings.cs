using System.Drawing;

namespace TagsCloudVisualization.FontSettings
{
    public interface IFontSettings
    {
        public FontFamily FontFamily { get; set; }
        public string FontColor { get; set; }
    }
}
