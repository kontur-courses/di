using Autofac;
using TagCloud;

namespace ConsoleApp;

internal static class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        var container = DiContainerBuilder.Build();

        var options = ArgumentsParser.ParseArgs(args);
        options.Apply(container.Resolve<ApplicationProperties>(), container.Resolve<IWordsParser>());
        if (options.OutputPath is null)
        {
            Console.WriteLine("Output path not set");
            return;
        }

        container.Resolve<TagCloudConstructor>().Construct().Save(options.OutputPath);
        Console.WriteLine($"Tag cloud saved to file {options.OutputPath}");
    }
}