namespace TagCloud.Domain.Settings;

public class TagCloudSettings
{
    public TagCloudSettings(LayoutSettings layoutSettings, FileSettings fileSettings, VisualizerSettings visualizerSettings, WordSettings wordSettings)
    {
        LayoutSettings = layoutSettings;
        FileSettings = fileSettings;
        VisualizerSettings = visualizerSettings;
        WordSettings = wordSettings;
    }
        
    public LayoutSettings LayoutSettings { get; }
    public FileSettings FileSettings { get; }
    public VisualizerSettings VisualizerSettings { get; }
    public WordSettings WordSettings { get; }
}