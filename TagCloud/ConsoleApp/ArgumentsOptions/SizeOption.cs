using CommandLine;

namespace ConsoleApp.ArgumentsOptions;

public class SizeOption : IArgumentsOption
{
    [Option('W', "width", Required = false, HelpText = "Image width")]
    public int Width { get; set; }

    [Option('H', "height", Required = false, HelpText = "Image height")]
    public int Height { get; set; }

    public void Execute()
    {
    }
}