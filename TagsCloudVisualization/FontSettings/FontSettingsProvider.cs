namespace TagsCloudVisualization.FontSettings;

public class FontSettingsProvider : IFontSettingsProvider
{
    private readonly int size;
    private readonly string family;

    public FontSettingsProvider(int size, string family)
    {
        this.size = size;
        this.family = family;
    }
    public FontSettings GetSettings()
    {
        return new FontSettings(size, family);
    }
}