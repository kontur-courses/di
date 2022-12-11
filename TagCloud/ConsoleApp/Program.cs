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
        
        var properties = container.Resolve<ApplicationProperties>();
        container.Resolve<TagCloudConstructor>().Construct().Save(properties.OutputPath);

        Console.WriteLine($"Tag cloud saved to file {properties.OutputPath}");
    }
}