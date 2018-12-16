using System;
using TagsCloudVisualization;
using System.Drawing;
using System.IO;
using System.Reflection;
using Autofac;
using TagsCloudVisualization.CloudGenerating;
using TagsCloudVisualization.ImageSaving;
using TagsCloudVisualization.Preprocessing;
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
                app.Run(arguments.ImageFileName, arguments.WordsFileName, arguments.Mode);
            }
            catch (Exception e)
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

            var pathToAssemblyDirectory = Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location);

            var builder = new ContainerBuilder();
            builder.RegisterType<SimpleFormatsReader>().As<IFileReader>();
            builder.RegisterType<FileReaderSelector>().AsSelf();

            builder.RegisterType<LinesParser>().As<IParser>();
            builder.RegisterType<LiteratureTextParser>().As<IParser>();
            builder.RegisterType<JsonParser>().As<IParser>();
            builder.RegisterType<ParserSelector>().AsSelf();

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
            builder.RegisterType<ToLowerTransformer>().As<IWordTransformer>();

            builder.RegisterType<StemmingTransformer>()
                .As<IWordTransformer>()
                .WithParameters(new[]
                {
                    new NamedParameter("dicFile", Path.Combine(
                            pathToAssemblyDirectory, "Resources", "en_US.dic")),
                    new NamedParameter("affFile", Path.Combine(
                            pathToAssemblyDirectory, "Resources", "en_US.aff")),
                });

            builder.RegisterType<TagsCloudGenerator>().As<ITagsCloudGenerator>();
            builder.RegisterType<Preprocessor>().AsSelf();
            builder.RegisterType<CustomPainter>().As<ITagPainter>();
            builder.RegisterType<TagsCloudVisualizer>().AsSelf();
            builder.RegisterType<StandardImageSaver>().As<IImageSaver>();
            builder.RegisterType<App>().AsSelf();
            builder.RegisterType<ImageSaverSelector>().AsSelf();
            return builder.Build();
        }
    }
}
