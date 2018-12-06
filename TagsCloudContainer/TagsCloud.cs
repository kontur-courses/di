using System;
using System.IO;
using Autofac;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer
{
    internal class TagsCloud
    {
        public static void Main(string[] args)
        {
            var configuration = new SimpleCommandLineParser().Parse(args);

            if (string.IsNullOrEmpty(configuration.PathToWordsFile)
                || string.IsNullOrEmpty(configuration.DirectoryToSave)
                || string.IsNullOrEmpty(configuration.OutFileName))
                return;

            var container = BuildContainer(configuration);

            var visualizer = container.Resolve<IVisualizer>();

            visualizer.Visualize();

            Console.WriteLine("Visualization has been saved to " +
                              Path.Combine(configuration.DirectoryToSave, configuration.OutFileName));

            Console.ReadKey();
        }

        private static IContainer BuildContainer(SimpleConfiguration configuration)
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(configuration)
                .As<IConfiguration>();
            builder.RegisterType<SimpleWordsReader>()
                .As<IWordsReader>();
            builder.RegisterType<SimplePreprocessor>()
                .As<IPreprocessor>();
            builder.RegisterType<SimpleWordCounter>()
                .As<IWordCounter>();
            builder.RegisterType<WordsGenerator>()
                .As<IWordsGenerator>();
            builder.RegisterType<CircularCloudLayouter>()
                .As<ICloudLayouter>();
            builder.RegisterType<TagsCloudVisualizer>()
                .As<IVisualizer>();

            return builder.Build();
        }
    }
}