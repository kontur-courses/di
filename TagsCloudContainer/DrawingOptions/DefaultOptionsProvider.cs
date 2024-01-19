using System.Drawing;

namespace TagsCloudContainer.DrawingOptions;

public class DefaultOptionsProvider : IOptionsProvider
{
    public Options Options => new()
    {
        FontColor = Color.White,
        BackgroundColor = Color.Black,
        Font = new Font(FontFamily.GenericSansSerif, 26),
        FrequencyScaling = 5,
        ImageSize = new Size(2000,2000)
    };
}