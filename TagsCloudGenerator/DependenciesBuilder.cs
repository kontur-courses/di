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
using TagsCloudGenerator.Tools;
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

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(x => typeof(IFileReader).IsAssignableFrom(x) && !x.IsAbstract)
                .AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<ConsoleClient>().As<IClient>().SingleInstance();
            builder.RegisterType<CyclicColoringAlgorithm>().As<IColoringAlgorithm>().SingleInstance();
            builder.RegisterType<CloudVisualizer>().As<ICloudVisualizer>().SingleInstance();
            builder.RegisterType<ImageSaver>().As<IImageSaver>().SingleInstance();
            builder.RegisterType<CloudGenerator>().As<ICloudGenerator>().SingleInstance();
            builder.RegisterType<WordHandler>().As<IWordHandler>().SingleInstance();
            builder.RegisterType<WordsParser>().As<IWordsParser>().SingleInstance();

            builder.RegisterInstance<ILayoutPointsGenerator>(new SpiralGenerator(center, 0.5, Math.PI / 16))
                .SingleInstance();
            builder.RegisterInstance<Predicate<KeyValuePair<string, int>>>(KeyLengthMoreThanTen)
                .SingleInstance();
            builder.RegisterInstance<Func<KeyValuePair<string, int>, KeyValuePair<string, int>>>(EmptyConvert)
                .SingleInstance();

            builder.RegisterType<PredicateFilter>().As<IWordsFilter>().SingleInstance();
            builder.RegisterType<FuncConverter>().As<IConverter>().SingleInstance();
            builder.RegisterType<LowercaseConverter>().As<IConverter>().SingleInstance();

            builder.RegisterType<BoringWordsEjector>()
                .As<IWordsFilter>()
                .As<BoringWordsEjector>()
                .SingleInstance();

            builder.RegisterType<InitialFormConverter>().As<IConverter>()
                .WithParameter(new NamedParameter("pathToAff", @"en-GB/index.aff"))
                .WithParameter(new NamedParameter("pathToDictionary", @"en-GB/index.dic"))
                .SingleInstance();

            builder.RegisterType<CircularCloudLayouter>()
                .WithParameter(new TypedParameter(typeof(Point), center))
                .WithParameter(new TypedParameter(typeof(Size), new Size(6, 10)))
                .As<ICloudLayouter>().SingleInstance();

            return builder.Build();
        }
    }
}