using CommandLine;

namespace TagCloud.App.UI.Console.Common;

public class AppSettings : IAppSettings
{
    [Option('w', "imageWidth", Default = 1000, HelpText = "Width of output image")]
    public int ImageWidth { get; set; } = 1000;

    [Option('h', "imageHeight", Default = 1000, HelpText = "Height of output image")]
    public int ImageHeight { get; set; } = 1000;

    [Option('i', "inputPath", Required = true, HelpText = "Path of input text")]
    public string InputPath { get; set; } = "input.txt";

    [Option('o', "outputPath", Default = "output", HelpText = "Path of output image")]
    public string OutputPath { get; set; } = "output.png";

    [Option('e', "outputFormat", Default = "png", HelpText = "Extension of output image")]
    public string OutputFormat { get; set; } = "png";

    [Option('f', "fontName", Default = "Roboto", HelpText = "Font used to display words")]
    public string FontName { get; set; } = "Roboto";

    [Option('l', "layouterName", Default = "Circular", HelpText = "Layouter used to place words")]
    public string LayouterName { get; set; } = "Circular";

    public static AppSettings Parse(string[] args)
    {
        var parsed = Parser.Default.ParseArguments<AppSettings>(args) as Parsed<AppSettings>;

        if (parsed == null)
            Environment.Exit(-1);

        return parsed.Value;
    }
}