using CommandLine;

namespace TagCloudConsoleApp;

// ReSharper disable UnusedAutoPropertyAccessor.Global
public class Options
{
    [Option('i', "input", Required = true, HelpText = "Input file.")]
    public string InputFile { get; set; } = null!;

    [Option('o', "output", Required = true, HelpText = "Output file.")]
    public string OutputFile { get; set; } = null!;

    [Option('k', "coloring-algorithm", Required = true, HelpText = "Coloring algorithm to use.\n"
                                                                   + "Options:\n"
                                                                   + "Random: colors each word with a random color. Default color set is rainbow."
                                                                   + "SingleColor: colors every word with one color. Default is red."
                                                                   + "Gradient: colors every word with colors from gradient from starting color to ending color. Default is from red to orange.")]
    public string ColoringAlgorithm { get; set; } = null!;
    
    [Option('w', "width", Required = true, HelpText = "Width of output file.")]
    public int Width { get; set; }

    [Option('h', "height", Required = false, HelpText = "Height of output file.\n"+
                                                        "Default is the same as width.")]
    public int? Height { get; set; }

    [Option('f', "font-name", Required = false, HelpText = "Font to use.\n"+
                                                           "Default is Arial.")]
    public string? FontName { get; set; }
    
    [Option('s', "font-size", Required = false, HelpText = "Font size to use.\n"+
                                                           "Default is 12pts.")]
    public int? FontSize { get; set; }
    
    [Option('c', "colors", Required = false, HelpText = "Colors to use.\n"+
                                                           "Refer to https://www.99colors.net/dot-net-colors for color names.\n"
                                                           +"Default depends on algorithm.")]
    public IEnumerable<string>? ColorNames { get; set; }
    
}
// ReSharper restore UnusedAutoPropertyAccessor.Global
