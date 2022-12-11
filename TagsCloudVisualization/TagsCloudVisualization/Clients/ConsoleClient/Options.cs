using CommandLine;

namespace TagsCloudVisualization.Clients;

public class Options
{
    [Option('p', "path", Required = false, Default = Config.DefaultPath, HelpText = "Path to input txt.")]
    public string Path { get; set; }

    [Option('w', "width", Required = false, Default = Config.WindowWidth, HelpText = "Window Width.")]
    public int ImageWidth { get; set; }

    [Option('h', "height", Required = false, Default = Config.WindowHeight, HelpText = "Window Height.")]
    public int ImageHeight { get; set; }
}