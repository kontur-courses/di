using System.Drawing;

namespace TagCloud.Visualization
{
    public class VisualizationSettings
    {
        public string FileName { get; set; } = "image";
        public string FontName { get; set; } = "Arial";
        public int FontSize { get; set; } = 16;

        public Font VisualizationFont => new Font(FontName, FontSize);
    }
}