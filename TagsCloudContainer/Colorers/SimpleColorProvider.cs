using System.Drawing;

namespace TagsCloudContainer.Colorers;

public class SimpleColorProvider : IColorProvider
{
    private readonly Color Color;

    public SimpleColorProvider(ISettingsProvider settingsProvider)
    {
        Color = settingsProvider.Settings.FontColor;
    }

    public Color ProvideColorForWord(string word, int frequency)
    {
        return Color;
    }
}