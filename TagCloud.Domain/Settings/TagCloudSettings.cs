namespace TagCloud.Domain.Settings;

public class TagCloudSettings
{
    public TagCloudSettings(LayoutSettings layoutSettings, PathSettings pathSettings, VisualizerSettings visualizerSettings)
    {
        LayoutSettings = layoutSettings;
        PathSettings = pathSettings;
        VisualizerSettings = visualizerSettings;
    }
        
    public LayoutSettings LayoutSettings { get; }
    public PathSettings PathSettings { get; }
    public VisualizerSettings VisualizerSettings { get; }
}