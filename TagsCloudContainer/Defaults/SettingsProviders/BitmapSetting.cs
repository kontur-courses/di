using Mono.Options;
using System.Drawing.Drawing2D;
using TagsCloudVisualization.Abstractions;

namespace TagsCloudContainer.Defaults.SettingsProviders;

public class BitmapSetting : ICliSettingsProvider
{
    public int Width { get; set; } = 800;

    public int Height { get; set; } = 400;

    public SmoothingMode SmoothingMode { get; set; } = SmoothingMode.AntiAlias;

    public OptionSet GetCliOptions()
    {
        var options = new OptionSet()
        {
            {"width=", $"Set width of resulting bitmap. Defaults to {Width}", (int v) => Width = v },
            {"height=", $"Set height of resulting bitmap. Defaults to {Height}",(int v) => Height = v },
            {"smoothing-mode=",$"Set smoothing mode for resulting bitmap. Defaults to {SmoothingMode}", (SmoothingMode v) => SmoothingMode = v },
        };

        return options;
    }
}
