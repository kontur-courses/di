using CommandLine;

namespace TagCloud.AppSettings;

public class Settings : IAppSettings
{
    [Option('s', "sourceFile", Default = "text.txt", HelpText = "Path to file with words to visualize")]
    public string InputPath { get; set; }

    [Option('o', "outputPath", Default = "result", HelpText = "Path to output image file")]
    public string OutputPath { get; set; }

    [Option('e', "extensionImage", Default = "png", HelpText = "Output image file format")]
    public string ImageExtension { get; set; }

    [Option('f', "fontType", Default = "SansSerif", HelpText = "Font type of words")]
    public string FontType { get; set; }

    [Option('W', "width", Default = 1920, HelpText = "Width of cloud")]
    public int CloudWidth { get; set; }

    [Option('H', "height", Default = 1080, HelpText = "Height of cloud")]
    public int CloudHeight { get; set; }

    [Option('l', "layouter", Default = "Spiral", HelpText = "Cloud layouter algorithm")]
    public string LayouterType { get; set; }

    [Option('d', "density", Default = 1, HelpText = "Density of cloud")]
    public int CloudDensity { get; set; }

    [Option('r', "randomPalette", Default = true, HelpText = "Use random colors")]
    public bool UseRandomPalette { get; set; }

    [Option("background", Default = "White", HelpText = "Cloud layouter algorithm")]
    public string BackgroundColor { get; set; }

    [Option("foreground", Default = "Black", HelpText = "Cloud layouter algorithm")]
    public string ForegroundColor { get; set; }

    [Option("boringWordsFile", Default = null, HelpText = "Cloud layouter algorithm")]
    public string BoringWordsFile { get; set; }
}