using CommandLine;

namespace TagsCloudVisualization.CLI;

public class Options
{
    public static string DefaultDirectory => Directory.GetCurrentDirectory();
    public static string DefaultOutputDirectory => Path.Combine(Directory.GetCurrentDirectory(),"Images");
    
    [Option('f', "file", HelpText = "Set absolute path to input file", Default = "text.txt")]
    public string Filepath { get; set; }
    
    [Option('d', "directory", HelpText = "Set output directory")]
    public string OutputDirectory { get; set; }

    [Option('h', "height", HelpText = "Set height of image", Default = 300)]
    public int Height { get; set; }

    [Option('w', "width", HelpText = "Set width of image", Default = 300)]
    public int Width { get; set; }

    [Option("extension", HelpText = "Set extension to image file", Default = "png")]
    public string ImageFileExtension { get; set; }
    
    [Option('c', "color-algorithm", HelpText = "Set color generation algorithm",Default = "rainbow")]
    public string ColorAlgorithm { get; set; }
    
    [Option('b', "background", HelpText = "Set background color of image",Default = "white")]
    public string BackgroundColor { get; set; }
    
    [Option("font-familty", HelpText = "Set font family",Default = "Arial")]
    public string FontFamily { get; set; }
    
    [Option("font-size", HelpText = "Set font size",Default = 30)]
    public int FontSize { get; set; }
    
    [Option("algorithm", HelpText = "Set layouter algoritm",Default = "circular")]
    public string LayoterAlgoritm { get; set; }
    
    [Option("tag-count", HelpText = "Set tag count",Default = 50)]
    public int TagCount { get; set; }
}