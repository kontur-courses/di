using System.Drawing;
using CommandLine;

namespace TagCloud.Clients;

public class CommandLineOptions
{
    [Option('i', "input", Required = true, HelpText = "Input file with words")]
    public string InputFile { get; set; }

    [Option('o', "output", Required = false, HelpText = "Output image")]
    public string OutputFile { get; set; }

    [Option('c', "colors", Required = false, Default = null, HelpText = "Colors to be used in image")]
    public IEnumerable<Color> Colors { get; set; }

    [Option('h', "height", Required = false, Default = 1080, HelpText = "Height of image")]
    public int Height { get; set; }

    [Option('w', "width", Required = false, Default = 1920, HelpText = "Width of image")]
    public int Width { get; set; }

    [Option("fontName", Required = false, Default = "Times New Roman", HelpText = "Font name")]
    public string FontName { get; set; }

    [Option("fontSize", Required = false, Default = 50, HelpText = "Font size")]
    public float FontSize { get; set; }

    [Option('a', "algorithm", Required = false, Default = "Spiral", HelpText = "Algorithm for generating image")]
    public string Curve { get; set; }
}