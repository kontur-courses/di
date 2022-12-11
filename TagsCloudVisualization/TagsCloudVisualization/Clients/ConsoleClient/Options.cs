using CommandLine;

namespace TagsCloudVisualization.Clients;

class Options
{
    [Option('p', "path", Required = true, HelpText = "Path to input txt.")]
    public string Path { get; set; }
}