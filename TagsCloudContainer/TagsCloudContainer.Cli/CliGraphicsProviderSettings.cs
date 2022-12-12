using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.Cli;

public class CliGraphicsProviderSettings : GraphicsProviderSettings
{
    public int Width { get; set; } = 1000;
    public int Height { get; set; } = 1000;

    public string BasePath { get; set; } =
        Path.Combine(AppContext.BaseDirectory, "images");
}