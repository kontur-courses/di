using Mono.Options;
using System.Drawing.Drawing2D;
using TagsCloudContainer.Abstractions;

namespace TagsCloudContainer.Defaults.SettingsProviders;

public class VisualizerSettings : ICliSettingsProvider
{
    private static int width = 800;
    private static int height = 400;
    private static SmoothingMode smoothingMode = SmoothingMode.AntiAlias;
    private static int wordLimit = 0;

    public int Width { get => width; private set => width = value; }
    public int Height { get => height; private set => height = value; }
    public SmoothingMode SmoothingMode { get => smoothingMode; private set => smoothingMode = value; }
    public int WordLimit { get => wordLimit; private set => wordLimit = value; }
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
