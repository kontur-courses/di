using System.Drawing;

namespace TagsCloudContainer.Colorers;

public class SimpleColorer : IColorer
{
    private readonly Color Color;
    public SimpleColorer(ISettingsProvider settingsProvider)
    {
        Color = settingsProvider.Settings.FontColor;
    }

    public Color ProvideColorForWord(string word, int frequency) => Color;
}