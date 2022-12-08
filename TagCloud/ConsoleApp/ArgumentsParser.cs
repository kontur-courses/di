using CommandLine;
using ConsoleApp.ArgumentsOptions;

namespace ConsoleApp;

public class ArgumentsParser
{
    private readonly Type[]? Options =
    {
        typeof(GuiModeOption),
        typeof(SizeOption)
    };

    public void ParseArgs(string[] args)
    {
        Parser.Default.ParseArguments<HelpOption>(args).WithParsed(o =>
        {
            o.AvailableOptionsTypes = Options;
            o.Execute();
        });

        Parser.Default.ParseArguments(args, Options).WithParsed(o =>
        {
            if (o is not IArgumentsOption asOption) return;
            asOption.Execute();
        });
    }
}