using CommandLine;

namespace TagCloudApplication;

public class Options
{
    [Option('p', "path", HelpText = "Set path.", Default = @"..\..\..\Fails")]
    public string Path { get; set; }
        
    [Option('c', "color", HelpText = "Set color.", Default = "random")]
    public string ColorScheme { get; set; }
}