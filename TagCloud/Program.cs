using System.Drawing;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using TagCloud.Clients;
using TagCloud.Curves;
using TagCloud.Files;
using TagCloud.Savers;
using TagCloud.Words;

namespace TagCloud;

internal static class Program
{
    public static void Main(string[] args)
    {
        var parsedArguments = Parser.Default.ParseArguments<CommandLineOptions>(args).Value;
        IServiceCollection services = new ServiceCollection();
        services.AddSingleton(x => IFile.GetByFileExtension(parsedArguments.InputFile));
        services.AddSingleton(x => ICurve.GetByName(parsedArguments.Curve));
        services.AddSingleton(x =>
            new CloudDrawer(new Size(parsedArguments.Width, parsedArguments.Height), parsedArguments.Colors));
        services.AddSingleton<CloudLayouter>();
        services.AddSingleton<TextFormatter>();
        services.AddSingleton<IBitmapSaver, HardDriveSaver>();
        services.AddSingleton<Client>();
        var container = services.BuildServiceProvider();

        var font = new Font(parsedArguments.FontName, parsedArguments.FontSize);
        var client = container.GetRequiredService<Client>();
        client.Draw(parsedArguments.OutputFile, font);
    }
}