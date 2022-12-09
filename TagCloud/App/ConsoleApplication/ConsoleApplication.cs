using Autofac;

namespace App.ConsoleApplication;

public class ConsoleApplication : IApplication
{
    public void Run(IContainer container, IEnumerable<string> args)
    {
        var argsParser = new ArgumentsParser();
        argsParser.ParseArgs(args);
        argsParser.Options?.Apply(container);
    }
}