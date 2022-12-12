using CommandLine;

namespace ConsoleApp;

public class ArgumentsParser
{
    public ConsoleOptions ParseArgs(IEnumerable<string> args)
    {
        var parseResult = new Parser(a => a.HelpWriter = Console.Error);
        return parseResult.ParseArguments<ConsoleOptions>(args).Value;
    }
}