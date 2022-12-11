using System.Drawing;
using System.Drawing.Imaging;
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
        var serviceProvider = DiContainerConfiguration.Build();
        var file = IFile.GetByFilename(parsedArguments.InputFile);
        var curve = ICurve.GetByName(parsedArguments.Curve);
        var font = new Font(parsedArguments.FontName, parsedArguments.FontSize);
        var size = new Size(parsedArguments.Width, parsedArguments.Height);
        var words = new TextFormatter().Format(file.ReadAll());
        
        var client = serviceProvider.GetRequiredService<Client>();
        var image = client.Draw(words, curve, size, font, parsedArguments.Colors);
        foreach (var outputFile in parsedArguments.OutputFiles)
        {
            var format = ImageFormatHelper.GetImageFormat(outputFile);
            client.Save(image, outputFile, format);
        }
    }
}