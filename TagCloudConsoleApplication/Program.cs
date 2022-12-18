using System.Drawing.Imaging;
using Autofac;
using CommandLine;
using TagCloudConsoleApplication.Options;
using TagCloudPainter.Savers;

namespace TagCloudConsoleApplication;

internal class Program
{
    private static void Main(string[] args)
    {
        Parser.Default.ParseArguments<TagCloudOptions>(args)
            .WithParsed(o =>
            {
                var container = new Configurator().Confiugre(o);

                var format = GetImageFormat(o.OutputPath);
                container.Resolve<ITagCloudSaver>().SaveTagCloud(o.InputPath, o.OutputPath, format);
            });
    }

    private static ImageFormat GetImageFormat(string output)
    {
        if (output.EndsWith(".png"))
            return ImageFormat.Png;
        if (output.EndsWith(".jpg") || output.EndsWith(".jpeg"))
            return ImageFormat.Jpeg;
        throw new ArgumentException("output is in not supported format", output);
    }
}