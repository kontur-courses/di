namespace TagsCloudVisualization.FontSettings;

public interface IFontSettingsProvider
{
    FontSettings GetSettings(int size, string family);
}