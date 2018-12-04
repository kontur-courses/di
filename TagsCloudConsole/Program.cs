using System;
using TagsCloudVisualization;
using System.Drawing;
using Autofac;
using TagsCloudVisualization.CloudGenerating;
using TagsCloudVisualization.Preprocessors;
using TagsCloudVisualization.Visualizing;

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
            app.Run();
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
                .As<IReader>()
                .WithParameter("fileName", arguments.WordsFile);

            builder.RegisterInstance(imageSettings).As<ImageSettings>();
            builder.RegisterType<ArchimedeanSpiralGenerator>()
                .As<ISpiralGenerator>()
                .WithParameters(new []
                {
                    new NamedParameter("center", new PointF(0, 0)),
                    new NamedParameter("step", 1),
                    new NamedParameter("angleDeltaInRadians", (float) (1 / (180 * Math.PI)))
                });
            builder.RegisterType<CircularCloudLayouter>().As<ILayouter>();
            builder.RegisterType<DullWordsFilter>().As<IFilter>();
            builder.RegisterType<BasicTransformer>().As<IWordTransformer>();
            builder.RegisterType<TagsCloudGenerator>().AsSelf();
            builder.RegisterType<CloudBuilder>().AsSelf();
            builder.RegisterType<CustomPainter>().As<ITagPainter>();
            builder.RegisterType<TagsCloudVisualizer>().AsSelf();
            builder.RegisterType<App>()
                .AsSelf()
                .WithParameter("outputFileName", arguments.OutputFileName);

            return builder.Build();
        }
    }
}
