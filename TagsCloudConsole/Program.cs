using System;
using TagsCloudVisualization;
using System.Drawing;
using Autofac;
using TagsCloudVisualization.CloudGenerating;
using TagsCloudVisualization.ImageSaving;
using TagsCloudVisualization.Preprocessors;
using TagsCloudVisualization.Visualizing;
using TagsCloudVisualization.WordsFileReading;

namespace TagsCloudConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new CustomParser();
            CustomArgs arguments;
            try
            {
                arguments = parser.Parse(args);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            var container = BuildContainer(arguments);
            var app = container.Resolve<App>();
            try
            {
                app.Run();
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static IContainer BuildContainer(CustomArgs arguments)
        {
            ImageSettings imageSettings = new ImageSettings()
            {
                BackgroundColor = arguments.BackgroundColor,
                TextColor = arguments.TextColor,
                FontName = arguments.FontName,
                ImageSize = arguments.ImageSize
            };

            var builder = new ContainerBuilder();
            builder.RegisterType<TextFileReader>()
                .As<IFileReader>();
            builder.RegisterType<FileReaderSelector>().AsSelf();

            builder.RegisterInstance(imageSettings).As<ImageSettings>();
            builder.RegisterType<ArchimedeanSpiralGeneratorFactory>()
                .As<ISpiralGeneratorFactory>()
                .WithParameters(new [] {
                    new NamedParameter("center", new PointF(0, 0)),
                    new NamedParameter("step", 1),
                    new NamedParameter("angleDeltaInRadians", (float) (1 / (180 * Math.PI)))
                });

            builder.RegisterType<CircularCloudLayouterFactory>()
                .As<ILayouterFactory>();

            builder.RegisterType<ArchimedeanSpiralGenerator>().AsSelf();
            builder.RegisterType<ArchimedeanSpiralGenerator>().As<ISpiralGenerator>();
            builder.RegisterType<CircularCloudLayouter>().As<ILayouter>();
            builder.RegisterType<DullWordsFilter>().As<IFilter>();
            builder.RegisterType<BasicTransformer>().As<IWordTransformer>();
            builder.RegisterType<TagsCloudGenerator>().As<ITagsCloudGenerator>();
            builder.RegisterType<CloudBuilder>().AsSelf();
            builder.RegisterType<CustomPainter>().As<ITagPainter>();
            builder.RegisterType<TagsCloudVisualizer>().AsSelf();
            builder.RegisterType<StandardImageSaver>().As<IImageSaver>();
            builder.RegisterType<App>()
                .AsSelf()
                .WithParameters(new[]
                {
                    new NamedParameter("imageFileName", arguments.OutputFileName),
                    new NamedParameter("imageFileExtension", arguments.ImageExtension),
                    new NamedParameter("wordsFileName", arguments.WordsFile),
                    new NamedParameter("wordsFileExtension", arguments.WordsFileExtension) 
                });
            builder.RegisterType<ImageSaverSelector>().AsSelf();
            return builder.Build();
        }
    }
}
