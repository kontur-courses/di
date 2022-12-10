using CommandLine;

namespace App.ConsoleApplication;

public class ArgumentsParser
{
    public ConsoleOptions? Options;
    
    public void ParseArgs(IEnumerable<string> args)
    {
        var p = new Parser(a => a.HelpWriter = Console.Error );
        p.ParseArguments<ConsoleOptions>(args).WithParsed(x => Options = x);
    }
}