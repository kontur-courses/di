using Mono.Options;
using System.Drawing.Drawing2D;
using TagsCloudContainer.Abstractions;

namespace TagsCloudContainer.Defaults.SettingsProviders;

public class VisualizerSettings : ICliSettingsProvider
{
    public int Width { get; private set; } = 800;
    public int Height { get; private set; } = 400;
    public SmoothingMode SmoothingMode { get; private set; } = SmoothingMode.AntiAlias;
    public int WordLimit { get; private set; } = 0;

    public OptionSet GetCliOptions()
    {
        var options = new OptionSet()
        {
            {"width=", $"Set width of resulting bitmap. Defaults to {Width}", (int v) => Width = v },
            {"height=", $"Set height of resulting bitmap. Defaults to {Height}",(int v) => Height = v },
            {"word-limit=", $"Set word limit to use. 0 means no limit Defaults to {WordLimit}",(int v) => WordLimit = v },
            {"smoothing-mode=",$"Set smoothing mode for resulting bitmap. Defaults to {SmoothingMode}", (SmoothingMode v) => SmoothingMode = v },
        };

        return options;
    }
}
