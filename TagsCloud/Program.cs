using Autofac;
using CommandLine;
using TagsCloud.App;
using TagsCloud.ConsoleCommands;

namespace TagsCloud;

public class Program
{
    static void Main(string[] args)
    {
        var options = CommandLine.Parser.Default.ParseArguments<Options>(args).Value;
        var container = ContainerConfig.Configure(options);

        using var scope = container.BeginLifetimeScope();

        var app = scope.Resolve<IApp>();
        app.Run();
    }
}