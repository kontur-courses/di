using Microsoft.Extensions.DependencyInjection;
using System.Drawing;
using TagsCloudContainer.CLI;
using TagsCloudContainer.FrequencyAnalyzers;
using TagsCloudContainer.TextTools;
using TagsCloudVisualization;

namespace TagsCloudContainer
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var services = DependencyInjectionConfig.AddCustomServices(new ServiceCollection());
            var serviceProvider = services.BuildServiceProvider();

            var reader = serviceProvider.GetService<TextFileReader>();
            var analyzer = serviceProvider.GetService<FrequencyAnalyzer>();

            if (args.Length < 1)
            {
                CommandLineArgs.PrintUsage();
                return;
            }

            var settings = CommandLineArgs.CreateSettingsObject(args);

            CommandLineArgs.ParseCommandLineArguments(settings.Item1, settings.Item2, args);


            string text = reader.ReadText(settings.Item2.textFile);

            analyzer.Analyze(text);

            var center = new Point(settings.Item1.Size.Width / 2, settings.Item1.Size.Height / 2);

            var pointsProvider = new SpiralPointsProvider(center);

            var layouter = new TagsCloudLayouter(center, pointsProvider, settings.Item1, analyzer.GetAnalyzedText());

            layouter.ToImage().Save(settings.Item2.outImagePath);
            Console.WriteLine("Resulting image saved to " + settings.Item2.outImagePath);
        }
    }
}