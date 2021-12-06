using System.Drawing.Drawing2D;
using TagsCloudVisualization.Abstractions;

namespace TagsCloudContainer.Defaults;

public class BitmapSetting : ISettingsProvider
{
    public int Width { get; set; } = 800;
    public int Height { get; set; } = 400;
    public SmoothingMode SmoothingMode { get; set; } = SmoothingMode.AntiAlias;
}