using System;
using System.Collections.Generic;
using System.Drawing;
using Autofac;
using TagsCloudContainer.Clients;
using TagsCloudContainer.Clients.CLI;
using TagsCloudContainer.Cloud;
using TagsCloudContainer.Core.Generators;
using TagsCloudContainer.Core.Layouters;
using TagsCloudContainer.Data.Processors;
using TagsCloudContainer.Data.Readers;
using TagsCloudContainer.Savers;
using TagsCloudContainer.Visualization;
using TagsCloudContainer.Visualization.Measurers;
using TagsCloudContainer.Visualization.Painters;

namespace TagsCloudContainer
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var container = CreateContainer();
            ConfigureServices(container.Resolve<ServiceSettings>());
            var clientFactory = container.Resolve<Func<string[], BaseClient>>();
            clientFactory(args).Run();
        }

        private static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<TagsCloudSettings>().AsImplementedInterfaces().AsSelf().SingleInstance();
            builder.RegisterType<ServiceSettings>().AsSelf().SingleInstance();

            builder.RegisterType<ArchimedeanSpiral>().As<IPointGenerator>()
                .UsingConstructor(typeof(ArchimedeanSpiral.ISettings));
            builder.RegisterType<CircularCloudLayouter>()
                .WithParameter("center", Point.Empty)
                .As<IRectangleLayouter>();

            builder.RegisterType<TxtWordsFileReader>().As<IWordsFileReader>();

            builder.RegisterType<LowerCaseWordProcessor>().Named<IWordProcessor>("lowerCaseProcessor");
            builder.Register(context =>
                {
                    var reader = context.Resolve<IWordsFileReader>();
                    var settings = context.Resolve<TagsCloudSettings>();
                    return new WordFilter(reader.ReadAllWords(settings.BoringWordsPath));
                })
                .Named<IWordProcessor>("wordFilter");
            builder.Register(c => new[]
            {
                c.ResolveNamed<IWordProcessor>("lowerCaseProcessor"),
                c.ResolveNamed<IWordProcessor>("wordFilter")
            }).As<IEnumerable<IWordProcessor>>();

            builder.RegisterType<ProbabilityWordMeasurer>().As<IWordMeasurer>()
                .UsingConstructor(typeof(ProbabilityWordMeasurer.ISettings));

            builder.RegisterType<ConstantColorsPainter>()
                .Named<IPainter>(typeof(ConstantColorsPainter).Name)
                .UsingConstructor(typeof(ConstantColorsPainter.ISettings));
            builder.RegisterType<SteppedColorPainter>()
                .Named<IPainter>(typeof(SteppedColorPainter).Name)
                .UsingConstructor(typeof(SteppedColorPainter.ISettings));
            builder.Register(c =>
            {
                var settings = c.Resolve<ServiceSettings>();
                var name = settings.GetService<IPainter>().Name;
                return c.ResolveNamed<IPainter>(name);
            }).As<IPainter>();

            builder.RegisterType<TagsCloudVisualizer>().AsSelf()
                .UsingConstructor(typeof(TagsCloudVisualizer.ISettings));

            builder.RegisterType<PngImageSaver>().As<IImageSaver>();

            builder.RegisterType<TagsCloud>().AsSelf();

            builder.RegisterType<ConsoleClient>().As<BaseClient>();

            return builder.Build();
        }

        private static void ConfigureServices(ServiceSettings settings)
        {
            settings.SetService<IPainter, SteppedColorPainter>();
        }
    }
}