using System.Drawing;
using TagCloud.Visualizer.Settings.Colorizer;

namespace TagCloud.Visualizer.Settings
{
    public interface IDrawSettings
    {
        Font Font { get; set; }
        IColorizer Colorizer { get; set; }
        Color Color { get; set; }
        DrawFormat DrawFormat { get; set; }
    }
}