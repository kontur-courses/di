using CommandLine;

namespace ConsoleApp.ArgumentsOptions;

public class GuiModeOption : IArgumentsOption
{
    [Option('g', "gui", Required = false, HelpText = "Run in GUI mode")]
    public bool IsRequired { get; set; }

    public void Execute()
    {
        Console.WriteLine("Loading GUI...");
    }
}