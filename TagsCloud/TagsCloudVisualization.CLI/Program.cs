using System;
using System.IO;
using System.Linq;
using Autofac;
using CommandLine;
using TagsCloudVisualization.Module;

namespace TagsCloudVisualization.CLI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var result = Parser.Default.ParseArguments<Options>(args);
                if (!result.Errors.Any())
                {
                    Run(result.Value);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void Run(Options options)
        {
            var container = new ContainerBuilder()
                            .RegisterTagsClouds(options.ToDrawerSettings())
                            .Build();
            var directory = Path.GetFullPath(options.OutputDirectory ?? Options.DefaultOutputDirectory);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var filename = Path.Combine(directory, options.OutputFileName ?? GenerateName());
            container.Resolve<TagsCloudVisualizer>().Visualize(filename);
            Console.WriteLine($"Tags cloud {filename} generated.");
        }

        private static string GenerateName() =>
            $"Cloud_{DateTime.Now:dd-MM-yy}_{DateTime.Now.Subtract(DateTime.Today).TotalSeconds:0}";
    }
}