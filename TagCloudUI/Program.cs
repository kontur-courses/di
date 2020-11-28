using System.Drawing;
using Autofac;
using TagCloud.Core.ColoringAlgorithms;
using TagCloud.Core.FileReaders;
using TagCloud.Core.ImageCreators;
using TagCloud.Core.ImageSavers;
using TagCloud.Core.LayoutAlgorithms;
using TagCloud.Core.WordConverters;
using TagCloud.Core.WordsFilters;
using TagCloud.Core.WordsProcessors;
using TagCloudUI.UI;

namespace TagCloudUI
{
    static class Program
    {
        public static void Main(string[] args)
        {
            var container = BuildContainer();
            var ui = container.Resolve<ConsoleUI>();
            ui.Run(args);
        }

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleUI>().AsSelf();
            builder.RegisterAssemblyTypes(typeof(TxtReader).Assembly)
                .As<IFileReader>();

            builder.RegisterAssemblyTypes(typeof(ToLowerConverter).Assembly)
                .As<IWordConverter>();
            builder.RegisterAssemblyTypes(typeof(BoringWordsFilter).Assembly)
                .As<IWordFilter>();
            builder.RegisterType<WordsProcessor>().As<IWordsProcessor>();

            builder.RegisterType<CircularCloudLayouter>().As<ILayoutAlgorithm>()
                .WithParameter("center", new Point(700, 700));
            builder.RegisterType<RainbowColoring>().As<IColoringAlgorithm>();
            builder.RegisterType<TagCloudImageCreator>().As<IImageCreator>();
            builder.RegisterType<AllExtensionsSaver>().As<IImageSaver>();

            return builder.Build();
        }
    }
}