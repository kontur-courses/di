using System.Drawing;
using MatthiWare.CommandLine;
using MatthiWare.CommandLine.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using RectanglesCloudLayouter.Core;
using RectanglesCloudLayouter.Interfaces;
using TagsCloudContainer.Interfaces;
using TagsCloudContainer.SettingsForTagsCloud;
using TagsCloudContainer.TagsCloudVisualization;
using TagsCloudContainer.TextProcessing;
using TagsCloudContainer.UserOptions;

namespace TagsCloudContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            InjectDependencies(serviceCollection, args);
            serviceCollection.BuildServiceProvider().GetService<AppProcessor>().Run();
        }

        private static void InjectDependencies(IServiceCollection serviceCollection, string[] args)
        {
            serviceCollection.AddSingleton<AppProcessor, AppProcessor>()
                .AddSingleton<ICommandLineParser<AllCommands>, CommandLineParser<AllCommands>>()
                .AddSingleton(typeof(string[]), args)
                .AddSingleton<ICloudSettings, CloudSettings>()
                .AddSingleton<ICloudParameter, CloudImageFormat>()
                .AddSingleton<ICloudParameter, BoringWords>()
                .AddSingleton<ICloudParameter, PathToCustomText>()
                .AddSingleton<ICloudParameter, PathToSaveCloud>()
                .AddSingleton<ICloudParameter, TextColor>()
                .AddSingleton<ICloudParameter, BackgroundColor>()
                .AddSingleton<ICloudParameter, ImageSize>()
                .AddSingleton<ICloudParameter, TextFont>()
                .AddSingleton<IVisualization, Visualization>()
                .AddSingleton<ITextAnalyzer, TextAnalyzer>()
                .AddSingleton(typeof(ICloudLayouter), new CloudLayouter(new Point()));
        }
    }
}