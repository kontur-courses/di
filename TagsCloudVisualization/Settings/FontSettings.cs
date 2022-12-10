using System.Drawing;

namespace TagsCloudVisualization.Settings
{
    public class FontSettings
    {
        public float MinEmSize { get; set; } = 6f;

        public float MaxEmSize { get; set; } = 24f;

        public string FontFamily { get; set; } = "Georgia";
        public FontStyle Style { get; set; } = FontStyle.Regular;
    }
}