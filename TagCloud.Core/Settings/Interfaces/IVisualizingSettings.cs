using System.Drawing;

namespace TagCloud.Core.Settings.Interfaces
{
    public interface IVisualizingSettings
    {
        int Width { get; set; }
        int Height { get; set; }
        string FontName { get; set; }
        float MinFontSize { get; set; }
        float MaxFontSize { get; set; }

        PointF CenterPoint { get; }
        Font DefaultFont { get; }
    }
}