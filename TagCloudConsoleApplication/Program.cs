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

                container.Resolve<ITagCloudSaver>().SaveTagCloud(o.OutputPath, o.InputPath);
            });
    }
}