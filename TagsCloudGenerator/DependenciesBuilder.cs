using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using Autofac;
using TagsCloudGenerator.Client;
using TagsCloudGenerator.Client.Console;
using TagsCloudGenerator.CloudLayouter;
using TagsCloudGenerator.FileReaders;
using TagsCloudGenerator.Saver;
using TagsCloudGenerator.Visualizer;
using TagsCloudGenerator.WordsHandler;
using TagsCloudGenerator.WordsHandler.Converters;
using TagsCloudGenerator.WordsHandler.Filters;

namespace TagsCloudGenerator
{
    public static class DependenciesBuilder
    {
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            var center = new Point(0, 0);
            bool KeyLengthMoreThanTen(KeyValuePair<string, int> pair) => pair.Key.Length > 10;
            KeyValuePair<string, int> EmptyConvert(KeyValuePair<string, int> pair) => pair;
            
            builder.RegisterType<ConsoleClient>().As<IClient>().SingleInstance();
            //builder.RegisterType<SimpleFileReader>().As<IFileReader>().SingleInstance();
            builder.RegisterType<XmlFileReader>().As<IFileReader>().SingleInstance();
            //builder.RegisterType<DocFileReader>().As<IFileReader>().SingleInstance();

            builder.RegisterType<CyclicColoringAlgorithm>().As<IColoringAlgorithm>().SingleInstance();
            builder.RegisterType<CloudVisualizer>().As<ICloudVisualizer>().SingleInstance();
            builder.RegisterType<ImageSaver>().As<IImageSaver>().SingleInstance();
            builder.RegisterType<CloudGenerator>().As<ICloudGenerator>().SingleInstance();
            builder.RegisterType<WordHandler>().As<IWordHandler>().SingleInstance();

            builder.RegisterInstance<ILayoutPointsGenerator>(new SpiralGenerator(center, 0.5, Math.PI / 16))
                .SingleInstance();
            builder.RegisterInstance<Predicate<KeyValuePair<string, int>>>(KeyLengthMoreThanTen).SingleInstance();
            builder.RegisterInstance<Func<KeyValuePair<string, int>, KeyValuePair<string, int>>>(EmptyConvert)
                .SingleInstance();

            builder
                .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(x => typeof(IWordsFilter).IsAssignableFrom(x))
                .AsImplementedInterfaces();

            builder
                .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(x => typeof(IConverter).IsAssignableFrom(x))
                .AsImplementedInterfaces();


            builder.RegisterType<CircularCloudLayouter>()
                .WithParameter(new TypedParameter(typeof(Point), center))
                .WithParameter(new TypedParameter(typeof(Size), new Size(6, 10)))
                .As<ICloudLayouter>();

            return builder.Build();
        }
    }
}