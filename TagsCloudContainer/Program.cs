using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using TagsCloudContainer.CLI;
using TagsCloudContainer.FrequencyAnalyzers;
using TagsCloudContainer.SettingsClasses;
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

            var appSettings = new AppSettings();

            Parser.Default.ParseArguments<CommandLineOptions>(args)
                .WithParsed(o => appSettings = CommandLineOptions.ParseArgs(o));

            string text = reader.ReadText(appSettings.TextFile);

            analyzer.Analyze(text);

            var layouter = new TagsCloudLayouter(
                appSettings.DrawingSettings.Size,
                appSettings.DrawingSettings.PointsProvider,
                appSettings.DrawingSettings,
                analyzer.GetAnalyzedText());

            layouter.ToImage().Save(appSettings.OutImagePath);
            Console.WriteLine("Resulting image saved to " + appSettings.OutImagePath);
        }
    }
}