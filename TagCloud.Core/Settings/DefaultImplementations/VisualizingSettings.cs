using System.Drawing;
using TagCloud.Core.Settings.Interfaces;

namespace TagCloud.Core.Settings.DefaultImplementations
{
    public class VisualizingSettings : IVisualizingSettings
    {
        public int Width { get; set; } = 800;
        public int Height { get; set; } = 600;
        public string FontName { get; set; } = "arial";
        public float MinFontSize { get; set; } = 15;
        public float MaxFontSize { get; set; } = 35;

        public PointF CenterPoint => new PointF((float)Width / 2, (float)Height / 2);
        public Font DefaultFont => new Font(FontName, (MaxFontSize + MinFontSize) / 2);
    }
}