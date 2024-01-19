using CommandLine;

namespace TagCloudApplication;

public class Options
{
    [Option('f', "filename", Required = true, HelpText = "Set filename.")]
    public string File { get; set; }
        
    [Option('c', "color", Required = true, HelpText = "Set filename.", Default = "r")]
    public string ColorScheme { get; set; }
}