using Autofac;
using TagCloud;

namespace ConsoleApp;

internal static class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        var container = DiContainerBuilder.Build();

        var argsParser = new ArgumentsParser();
        argsParser.ParseArgs(args);
        argsParser.Options?.Apply(container);
        if (argsParser.Options is null)
            return;

        if (argsParser.Options.OutputPath is null)
        {
            Console.WriteLine("Output path not set");
            return;
        }

        container.Resolve<TagCloudConstructor>().Construct().Save(argsParser.Options.OutputPath);
        Console.WriteLine($"Tag cloud saved to file {argsParser.Options.OutputPath}");
    }
}