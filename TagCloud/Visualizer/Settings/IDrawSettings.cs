using System.Drawing;

namespace TagCloud.Visualizer.Settings
{
    public interface IDrawSettings
    {
        Brush Brush { get; set; }
        Font Font { get; set; }
        DrawFormat DrawFormat { get; set; }
    }
}