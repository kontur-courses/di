namespace TagCloud.Settings;

public interface IAppSettings
{
    public string InputPath { get; }
    public string OutputPath { get; }
    public string ImageExtension { get; }
    public string FontType { get; }
    public int CloudWidth { get; }
    public int CloudHeight { get; }
    public string LayouterType { get; }
}