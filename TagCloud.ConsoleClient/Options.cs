using CommandLine;
using System.Drawing;

public class Options
{
    [Option('i', "input", Required = true, HelpText = "Path to input text")]
    public string InputTextPath{ get; set; }

    [Option('o', "output", Required = true, HelpText = "Output image path")]
    public string OutputImagePath { get; set; }

    [Option('c', "center", Required = false, HelpText = "Center of tag cloud (f.e. 200x200)")]
    public string? TagCloudCenter { get; set; }

    [Option('s', "size", Required = false, HelpText = "Size of a result image (f.e. 200x200)")]
    public string? ImageSize { get; set; }

    [Option('f', "fsize", Required = false, HelpText = "Fontsize span (f.e 12:32)")]
    public string? FontSize { get; set; }
    [Option('t', "tcount", Required = false, HelpText = "Number of tags to take")]
    public string? TagsCount { get; set; }
    [Option('F', "family", Required = false, HelpText = "Font family (Arial default)")]
    public string? FontFamily { get; set; }

    
}