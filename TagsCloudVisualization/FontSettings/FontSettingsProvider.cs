namespace TagsCloudVisualization.FontSettings;

public class FontSettingsProvider : IFontSettingsProvider
{
    public FontSettings GetSettings(int size, string family)
    {
        return new FontSettings(size, family);
    }
}