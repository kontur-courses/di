using CommandLine;

namespace ConsoleClient;

public class Options
{
    [Option('s', "source", Required = false, HelpText = "Source filepath")]
    public string? Source { get; set; }

    [Option('r', "result", Required = false, HelpText = "Result filepath")]
    public string? Result { get; set; }
}