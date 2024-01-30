using System.Diagnostics;
using Autofac;
using CommandLine;
using DocumentFormat.OpenXml.Spreadsheet;
using TagsCloudContainer.App;
using TagsCloudContainer.App.Interfaces;

namespace TagsCloudContainer
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var options = Parser.Default.ParseArguments<CommandLineOptions>(args).Value;
            var container = Container.SetDiBuilder(options);
            var app = container.Resolve<IApp>();
            app.Run();
        }
    }
}