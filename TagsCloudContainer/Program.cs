using System.Drawing;
using System.IO;
using MatthiWare.CommandLine;
using Microsoft.Extensions.DependencyInjection;
using RectanglesCloudLayouter.Core;
using RectanglesCloudLayouter.Interfaces;
using TagsCloudContainer.ConvertersAndCheckers;
using TagsCloudContainer.Interfaces;
using TagsCloudContainer.Reader;
using TagsCloudContainer.Saver;
using TagsCloudContainer.Settings;
using TagsCloudContainer.TagsCloudVisualization;
using TagsCloudContainer.TextProcessing;
using TagsCloudContainer.UserOptions;

namespace TagsCloudContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            var parsed = new CommandLineParser<AllUserCommands>().Parse(args);
            if (parsed.HasErrors)
                return;
            var temporarySettingsStorage = new TemporarySettingsStorage(parsed.Result);
            var serviceCollection = new ServiceCollection();
            InjectDependencies(serviceCollection, temporarySettingsStorage);
            serviceCollection.BuildServiceProvider().GetRequiredService<IAppProcessor>().Run();
        }

        private static void InjectDependencies(IServiceCollection serviceCollection,
            TemporarySettingsStorage settingsStorage)
        {
            serviceCollection.AddSingleton<IAppProcessor, AppProcessor>()
                .AddSingleton<IFileReader, FileReader>()
                .AddSingleton<IImageSaver, ImageSaver>()
                .AddSingleton(typeof(IStorageSettings),
                    new StorageSettings(settingsStorage.PathToCustomText, settingsStorage.PathToSave,
                        settingsStorage.ImageFormat))
                .AddSingleton(typeof(IVisualizationSettings),
                    new VisualizationSettings(settingsStorage.ImageSize, settingsStorage.BackgroundColor,
                        settingsStorage.TextColor, settingsStorage.Font))
                .AddSingleton(typeof(ITextProcessingSettings), new TextProcessingSettings(settingsStorage.BoringWords))
                .AddSingleton<IVisualization, Visualization>()
                .AddSingleton<ICloudLayouter, CloudLayouter>()
                .AddSingleton(typeof(ISpiral), new ArchimedeanSpiral(new Point(0, 0)))
                .AddSingleton<ICloudRadiusCalculator, CloudRadiusCalculator>()
                .AddSingleton<IWordTagsLayouter, WordTagsLayouter>()
                .AddSingleton(typeof(Font), settingsStorage.Font)
                .AddSingleton<IWordsFrequency, WordsFrequency>()
                .AddSingleton<IWordMeasurer, WordMeasurer>()
                .AddSingleton<IWordsFilter, WordsFilter>()
                .AddSingleton<ISpeechPartsParser, SpeechPartsParser>()
                .AddSingleton(typeof(ITextConverter),
                    new MyStemConverter(Path.GetFullPath("mystem.exe"), "-ni"));
        }
    }
}