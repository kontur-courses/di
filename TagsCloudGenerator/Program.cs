using System.Drawing;
using System.Reflection;
using Autofac;
using TagsCloudGenerator.Client;
using TagsCloudGenerator.Client.Console;
using TagsCloudGenerator.Visualizer;
using TagsCloudGenerator.CloudLayouter;
using TagsCloudGenerator.FileReaders;
using TagsCloudGenerator.Saver;
using TagsCloudGenerator.WordsHandler;

namespace TagsCloudGenerator
{
    class Program
    {
        private static void Main(string[] args)
        {
            using (var container = BuildContainer())
            {
                var client = container.Resolve<IClient>();
                var generator = container.Resolve<ICloudGenerator>();

                client.Run(generator);
            }
        }

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ConsoleClient>().As<IClient>().SingleInstance();
            builder.RegisterType<SimpleFileReader>().As<IFileReader>().SingleInstance();
            builder.RegisterType<CyclicColoringAlgorithm>().As<IColoringAlgorithm>().SingleInstance();
            builder.RegisterType<CloudVisualizer>().As<ICloudVisualizer>().SingleInstance();
            builder.RegisterType<ImageSaver>().As<IImageSaver>().SingleInstance();
            builder.RegisterType<CloudGenerator>().As<ICloudGenerator>().SingleInstance();
            builder.RegisterType<WordHandler>().As<IWordHandler>().SingleInstance();

            builder
                .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(x => typeof(IWordsFilter).IsAssignableFrom(x))
                .AsImplementedInterfaces();

            builder
                .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(x => typeof(IConverter).IsAssignableFrom(x))
                .AsImplementedInterfaces();


            builder.RegisterType<CircularCloudLayouter>()
                .WithParameter(new TypedParameter(typeof(Point), new Point(0, 0)))
                .WithParameter(new TypedParameter(typeof(Size), new Size(6, 10)))
                .As<ICloudLayouter>();

            return builder.Build();
        }

    }
}