using System;
using System.Drawing;
using System.Linq;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using TagsCloudContainer.CloudLayouter;
using TagsCloudContainer.Drawing;
using TagsCloudContainer.FileReaders;
using TagsCloudContainer.ImageSaver;
using TagsCloudContainer.Settings;
using TagsCloudContainer.WordFilters;
using TagsCloudContainer.WordsConverter;

namespace TagsCloudContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            var parsedArgs = Parser.Default.ParseArguments<AppSettings>(args);
            if (parsedArgs.Errors.Any())
            {
                throw new ArgumentException("Errors occured while parsing arguments");
            }

            var appSettings = parsedArgs.Value;

            var container = GetConfiguredContainer(appSettings);
            var tagCloudCreator = container.GetService<TagCloudCreator>();
            tagCloudCreator.CreateTagCloudImage();
        }

        private static ServiceProvider GetConfiguredContainer(IAppSettings appSettings)
        {
            var container = new ServiceCollection();
            container.AddSingleton<IFileReader, TxtReader>();
            container.AddSingleton<IDrawer, TagCloudDrawer>();
            container.AddSingleton<IWordsFilter, BoringWordsFilter>();
            container.AddSingleton<IWordConverter, WordToTagConverter>();
            container.AddSingleton<IImageSaver, ImageSaver.ImageSaver>();
            container.AddSingleton<TagCloudCreator, TagCloudCreator>();
            container.AddSingleton<ICloudLayouter, CircularCloudLayouter>();
            container.AddSingleton<IFontCreator>(new FontCreator(appSettings.FontName));
            container.AddSingleton<ISpiral>(new ArchimedeanSpiral(Point.Empty));
            container.AddSingleton(appSettings);

            return container.BuildServiceProvider();
        }
    }
}