using System.Drawing;

namespace TagsCloudVisualization.AppSettings
{
    public class FontSettings
    {
        public Font Font { get; set; } = new Font(FontFamily.GenericSansSerif, 16);
        public float MinSize { get; set; } = 10;
        public float MaxSize { get; set; } = 60;
    }
}