using CommandLine;

namespace ConsoleClient;

public class Options
{
    [Value(0, Required = true, HelpText = "Source filepath", MetaValue = nameof(String))]
    public string Source { get; set; } = "";

    [Value(1, Required = true, HelpText = "Result filepath", MetaValue = nameof(String))]
    public string Result { get; set; } = "";

    [Option('w', "width", Default = 800, Required = false,
        HelpText = "Result image width", MetaValue = nameof(Int32))]
    public int ImageWidth { get; set; }

    [Option('h', "height", Default = 600, Required = false,
        HelpText = "Result image height", MetaValue = nameof(Int32))]
    public int ImageHeight { get; set; }

    [Option('b', "background", Default = "White", Required = false,
        HelpText = "Background color name", MetaValue = nameof(String))]
    public string BackgroundColorName { get; set; } = "";

    [Option('c', "color", Default = "Black", Required = false,
        HelpText = "Background color name", MetaValue = nameof(String))]
    public string TextColorName { get; set; } = "";

    [Option('f', "font", Default = "Arial", Required = false,
        HelpText = "Font family name", MetaValue = nameof(String))]
    public string FontFamilyName { get; set; } = "";

    [Option('M', "maxFontSize", Default = 50, Required = false,
        HelpText = "Maximum font size", MetaValue = nameof(Int32))]
    public int MaxFontSize { get; set; }

    [Option('m', "minFontSize", Default = 10, Required = false,
        HelpText = "Minimum font size", MetaValue = nameof(Int32))]
    public int MinFontSize { get; set; }
    
    [Option('x', "xFlattening", Default = 1, Required = false,
        HelpText = "Cloud flattening along the X axis", MetaValue = nameof(Double))]
    public double XFlattening { get; set; }
}