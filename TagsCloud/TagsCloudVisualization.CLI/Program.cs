using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Autofac;
using CommandLine;

namespace TagsCloudVisualization.CLI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                Run(Parser.Default.ParseArguments<Options>(args));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void Run(ParserResult<Options> result)
        {
            if (result.Errors.OfType<HelpRequestedError>().Any()) return;
            if (result.Errors.Any()) throw new ArgumentException(result.Errors.First().ToString());
            var options = result.Value;

            var container = CreateContainer(options.ToDrawerSettings());
            var directory = Path.GetFullPath(options.OutputDirectory ?? Options.DefaultOutputDirectory);
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
            var filename =
                Path.Combine(directory, options.OutputFileName ?? DateTime.Now.ToString(CultureInfo.InvariantCulture));
            container.Resolve<TagsCloudVisualizer>().Visualize(filename);

            Console.WriteLine($"Tags cloud {filename} generated.");
        }

        private static IContainer CreateContainer(TagsCloudDrawerModuleSettings settings)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new TagsCloudDrawerModule(settings));
            return builder.Build();
        }
    }
}