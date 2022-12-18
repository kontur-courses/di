using System.Drawing;
using CommandLine;

namespace TagCloudConsoleApplication.Options;

public class TagCloudOptions
{
    [Option('w', "width", Required = false, HelpText = "Set image width", Default = 500)]
    public int ImageWidth { get; set; }

    [Option('h', "height", Required = false, HelpText = "Set image height", Default = 500)]
    public int ImageHeight { get; set; }

    [Option('i', "inputPath", Required = true, HelpText = "Set path to input file")]
    public string InputPath { get; set; } = string.Empty;

    [Option('o', "outputPath", Required = true, HelpText = "Set path to output file")]
    public string OutputPath { get; set; } = string.Empty;

    [Option("fontFamily", Required = false, HelpText = "Set tags font name", Default = "Arial")]
    public string FontFamily { get; set; }

    [Option("fontSize", Required = false, HelpText = "Set tags size to font", Default = 7)]
    public int FontSize { get; set; }

    [Option("backgroundColor", Required = false, HelpText = "Set background color to image (Default = Black)")]
    public Color BackgroundColor { get; set; } = Color.Black;

    [Option("ColorAlgorithm", HelpText = "Set algorithm to coloring words", Default = "White")]
    public string WordColoring { get; set; }

    [Option('r', "radius", Required = false, HelpText = "Set radius step to layouter", Default = 0.25)]
    public double Radius { get; set; }

    [Option('a', "angle", Required = false, HelpText = "Set angle step to layouter", Default = Math.PI / 12)]
    public double Angle { get; set; }

    [Option('x', "xCoord", Required = false, HelpText = "Set x coord to center point", Default = 250)]
    public int XCenter { get; set; }

    [Option('y', "yCoord", Required = false, HelpText = "Set y coord to center point", Default = 250)]
    public int YCenter { get; set; }
}