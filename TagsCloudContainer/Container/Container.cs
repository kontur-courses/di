using System.Drawing;
using Autofac;
using TagsCloudContainer.Application;
using TagsCloudContainer.Layouter;
using TagsCloudContainer.TextReaders;
using TagsCloudContainer.Visualisators;
using TagsCloudContainer.WorkWithWords;

namespace TagsCloudContainer.Container
{
    public static class Container
    {
        public static IContainer SetDiBuilder(Options options)
        {
            var builder = new ContainerBuilder();
            builder.Register(x => new Settings()
                {
                    WordColor = Color.FromName(options.ColorName),
                    WordFontName = options.FontName,
                    WordFontSize = options.FontSize,
                    FileName = options.PathToInputFile,
                    BoringWordsFileName = options.PathToBoringWordsFile
                })
                .As<Settings>();
            builder.RegisterType<TextReaderGenerator>().AsSelf();
            builder.RegisterType<WordHandler>().AsSelf();
            builder.Register(x =>
                    new CircularCloudLayouter(new Point(options.CenterX, options.CenterY)))
                .As<CircularCloudLayouter>();
            builder.RegisterType<RectangleVisualisator>().As<IVisualisator>();
            builder.RegisterType<ConsoleApp>().As<IApp>();

            return builder.Build();
        }
    }
}