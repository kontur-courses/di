namespace TagCloud.Domain.Settings;

public class TagCloudSettings
{
    public TagCloudSettings(LayoutSettings layoutSettings, FileSettings fileSettings, VisualizerSettings visualizerSettings)
    {
        LayoutSettings = layoutSettings;
        FileSettings = fileSettings;
        VisualizerSettings = visualizerSettings;
    }
        
    public LayoutSettings LayoutSettings { get; }
    public FileSettings FileSettings { get; }
    public VisualizerSettings VisualizerSettings { get; }
}