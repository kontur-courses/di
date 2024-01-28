﻿using CommandLine;

namespace TagCloudApplication;

public class Options
{
    [Option('d', "destination", HelpText = "Set destination path.", Default = @"..\..\..\Results")]
    public string DestinationPath { get; set; }
    
    [Option('s', "source", HelpText = "Set source path.", Default = @"..\..\..\Results\text.txt")]
    public string SourcePath { get; set; }
    
    [Option('n', "name", HelpText = "Set name.", Default = "default")]
    public string Name { get; set; }
        
    [Option('c', "color", HelpText = "Set color.", Default = "random")]
    public string ColorScheme { get; set; }
    
    [Option('f', "font", HelpText = "Set font.", Default = "Arial")]
    public string Font { get; set; }
    
    [Option("size", HelpText = "Set font size.", Default = 20)]
    public int FontSize { get; set; }
    
    [Option("width", HelpText = "Set width.", Default = 100)]
    public int Width { get; set; }
    
    [Option("height", HelpText = "Set height.", Default = 100)]
    public int Height { get; set; }
}