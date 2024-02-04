using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using TagsCloudContainer.CLI;
using TagsCloudContainer.Drawer;
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

            var reader = serviceProvider.GetService<ITextReader>();
            var analyzer = serviceProvider.GetService<IAnalyzer>();

            var appSettings = new AppSettings();

            Parser.Default.ParseArguments<CommandLineOptions>(args)
                .WithParsed(o => appSettings = CommandLineOptions.ParseArgs(o));

            var text = reader.ReadText(appSettings.TextFile);
            var layouter = serviceProvider.GetService<TagsCloudLayouter>();

            analyzer.Analyze(text, appSettings.FilterFile);

            layouter.Initialize(appSettings.DrawingSettings, analyzer.GetAnalyzedText());

            Visualizer.Draw(appSettings.DrawingSettings.Size,
                            layouter.GetTextImages(),
                            appSettings.DrawingSettings.BgColor)
                .Save(appSettings.OutImagePath);
            Console.WriteLine("Resulting image saved to " + appSettings.OutImagePath);
        }
    }
}