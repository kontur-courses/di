using CommandLine;

namespace ConsoleApp;

public static class ArgumentsParser
{
    public static ConsoleOptions? ParseArgs(string[] args)
    {
        var parseResult = new Parser(a => a.HelpWriter = Console.Error);
        return parseResult.ParseArguments<ConsoleOptions>(args).Value;
    }
}