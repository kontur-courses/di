using TagsCloudContainer;

namespace TagsCloudWinformsApp;

internal class SettingsHandler : ISettingsProvider
{
    public Settings LocalSettings = new()
    {
        BackgroundColor = Color.Black,
        FontColor = Color.Cyan,
        Font = new Font(FontFamily.GenericSerif, 26),
        FrequencyGrowth = 5,
        ImageSize = new Size(1000, 1000)
    };

    public Settings Settings =>
        new()
        {
            BackgroundColor = LocalSettings.BackgroundColor,
            FontColor = LocalSettings.FontColor,
            Font = LocalSettings.Font,
            FrequencyGrowth = LocalSettings.FrequencyGrowth,
            ImageSize = LocalSettings.ImageSize
        };
}