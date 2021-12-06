using System;
using System.Linq;
using Autofac;
using CommandLine;
using TagsCloud.Visualization;
using TagsCloud.Visualization.Drawer;
using TagsCloud.Visualization.ImagesSavior;
using TagsCloud.Visualization.LayoutContainer.ContainerBuilder;

namespace TagsCloud.Words
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var result = Parser.Default.ParseArguments<Options>(args);
            if (result.Errors.OfType<HelpRequestedError>().Any())
                return;
            if (result.Errors.Any())
                throw new ArgumentException(result.Errors.First().ToString());

            TagsCloudModuleSettings settings;
            try
            {
                settings = new SettingsCreator().Create(result.Value);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            using var container = CreateContainer(settings).BeginLifetimeScope();

            var parsedWords = container.Resolve<IWordsService>()
                .GetWords();

            var maxCount = parsedWords.Max(x => x.Count);
            var minCount = parsedWords.Min(x => x.Count);

            var wordsContainer = container.Resolve<AbstractWordsContainerBuilder>()
                .AddWords(parsedWords, minCount, maxCount)
                .Build();

            var drawer = container.Resolve<IDrawer>();
            using var image = drawer.Draw(wordsContainer);
            container.Resolve<IImageSavior>().Save(image);
        }

        private static IContainer CreateContainer(TagsCloudModuleSettings settings)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new TagsCloudModule(settings));
            return builder.Build();
        }
    }
}