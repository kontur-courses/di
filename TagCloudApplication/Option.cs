using CommandLine;

namespace TagCloudApplication;

public class Options
{
    [Option('d', "destination", HelpText = "Set path.", Default = @"..\..\..\Fails")]
    public string DestinationPath { get; set; }
    
    [Option('s', "source", HelpText = "Set path.", Default = @"..\..\..\Fails\text.txt")]
    public string SourcePath { get; set; }
    
    [Option('n', "name", HelpText = "Set name.", Default = "default")]
    public string Name { get; set; }
        
    [Option('c', "color", HelpText = "Set color.", Default = "random")]
    public string ColorScheme { get; set; }
    
    [Option('f', "font", HelpText = "Set font.", Default = "Comic Sans")]
    public string Font { get; set; }
    
    [Option("size", HelpText = "Set font.", Default = 20)]
    public int FontSize { get; set; }
}