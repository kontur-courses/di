using CommandLine;

namespace ConsoleApp;

public class ConsoleOptions
{
    [Option('f', "file", Required = true, HelpText = "Image file to read words")]
    public string? File { get; set; }

    [Option('F', "font", Required = false, HelpText = "Font name", Default = "Arial")]
    public string FontName { get; set; }

    [Option("minfont", Required = false, Default = 11, HelpText = "Minimum font size")]
    public int MinFont { get; set; }

    [Option("maxfont", Required = false, Default = 64, HelpText = "Maximum font size")]
    public int MaxFont { get; set; }

    [Option('W', "width", Required = true, HelpText = "Image width")]
    public int Width { get; set; }

    [Option('H', "height", Required = true, HelpText = "Image height")]
    public int Height { get; set; }

    [Option('d', "density", Required = false, Default = 0.1, HelpText = "Cloud density")]
    public double Density { get; set; }

    [Option("background", Required = false, Default = "#000000", HelpText = "Background color")]
    public string BackgroundColor { get; set; }

    [Option("foreground", Required = false, Default = "#ffffff", HelpText = "Foreground color")]
    public string ForegroundColor { get; set; }

    [Option('o', "out", Required = false, Default = "Cloud.png",
        HelpText = "Output image path(including name). Supported formats: .jpg, .jpeg, .png")]
    public string OutputPath { get; set; }

    [Option("exclude", Required = false, HelpText = "Words to exclude")]
    public string? ExcludedWords { get; set; }
}