using CommandLine;

namespace App.ConsoleApplication;

public class ArgumentsParser
{
    public ConsoleOptions? Options;

    public void ParseArgs(IEnumerable<string> args)
    {
        var success = Parser.Default
            .ParseArguments<ConsoleOptions>(args)
            .WithParsed(x => Options = x);
        
        if (success.Errors.Any())
            throw new ArgumentException("Wrong console arguments");
    }
}