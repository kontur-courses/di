using System;
using System.Drawing;
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
            var appSettings = parsedArgs.Value;

            try
            {
                var container = GetConfiguredContainer(appSettings);
                var tagCloudCreator = container.GetService<TagCloudCreator>();
                tagCloudCreator.CreateTagCloudImage();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static ServiceProvider GetConfiguredContainer(IAppSettings appSettings)
        {
            var container = new ServiceCollection();
            container.AddSingleton<IFileReader, TxtFileReader>();
            container.AddSingleton<IFileReader, DocFileReader>();
            container.AddSingleton<IDrawer, TagCloudDrawer>();
            container.AddSingleton<IWordsFilter, BoringWordsFilter>();
            container.AddSingleton<IWordConverter, WordToTagConverter>();
            container.AddSingleton<IImageSaver, ImageSaver.ImageSaver>();
            container.AddSingleton<TagCloudCreator, TagCloudCreator>();
            container.AddSingleton<ICloudLayouter, CircularCloudLayouter>();
            container.AddSingleton<IFontCreator, FontCreator>();
            container.AddSingleton<ISpiral, ArchimedeanSpiral>();
            container.AddSingleton(appSettings);

            return container.BuildServiceProvider();
        }
    }
}