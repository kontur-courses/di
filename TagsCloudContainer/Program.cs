using System;
using System.Drawing;
using Autofac;
using NHunspell;
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
            var clientFactory = container.Resolve<Func<string[], BaseClient>>();
            clientFactory(args).Run();
        }

        private static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<TagsCloudSettings>().AsImplementedInterfaces().AsSelf().SingleInstance();

            builder.RegisterType<ArchimedeanSpiral>().As<IPointGenerator>()
                .UsingConstructor(typeof(ArchimedeanSpiral.ISettings));
            builder.RegisterType<CircularCloudLayouter>()
                .WithParameter("center", Point.Empty)
                .Named<IRectangleLayouter>(CircularCloudLayouter.Name);
            builder.Register(c =>
            {
                var settings = c.Resolve<TagsCloudSettings>();
                return c.TryResolveNamed(settings.Layouter, typeof(IRectangleLayouter), out var layouter)
                    ? layouter
                    : c.ResolveNamed<IRectangleLayouter>(CircularCloudLayouter.Name);
            }).As<IRectangleLayouter>();

            builder.RegisterType<TxtWordsFileReader>().As<IFileFormatReader>();
            builder.RegisterType<DocWordsFileReader>().As<IFileFormatReader>();
            builder.RegisterType<WordsFileReader>().As<IWordsFileReader>();

            builder.Register(c =>
            {
                var settings = c.Resolve<TagsCloudSettings>();
                return new Hunspell(settings.AffFile, settings.DicFile);
            }).As<Hunspell>();
            builder.RegisterType<WordStemProcessor>().As<IWordProcessor>();
            builder.Register(context =>
            {
                var reader = context.Resolve<IWordsFileReader>();
                var settings = context.Resolve<TagsCloudSettings>();
                return new WordFilter(reader.ReadAllWords(settings.BoringWordsPath));
            }).As<IWordProcessor>();
            builder.RegisterType<LowerCaseWordProcessor>().As<IWordProcessor>();

            builder.RegisterType<ProbabilityWordMeasurer>().As<IWordMeasurer>()
                .UsingConstructor(typeof(ProbabilityWordMeasurer.ISettings));

            builder.RegisterType<ConstantColorsPainter>()
                .Named<IPainter>(ConstantColorsPainter.Name)
                .UsingConstructor(typeof(ConstantColorsPainter.ISettings));
            builder.RegisterType<SteppedColorPainter>()
                .Named<IPainter>(SteppedColorPainter.Name)
                .UsingConstructor(typeof(SteppedColorPainter.ISettings));
            builder.Register(c =>
            {
                var settings = c.Resolve<TagsCloudSettings>();
                return c.TryResolveNamed(settings.Painter, typeof(IPainter), out var painter)
                    ? painter
                    : c.ResolveNamed<IPainter>(SteppedColorPainter.Name);
            }).As<IPainter>();

            builder.RegisterType<TagsCloudVisualizer>().AsSelf()
                .UsingConstructor(typeof(TagsCloudVisualizer.ISettings));

            builder.RegisterType<DefaultImageSaver>().As<IImageSaver>();
            builder.RegisterType<FileImageSaver>().AsSelf();

            builder.RegisterType<TagsCloudGenerator>().AsSelf();

            builder.RegisterType<ConsoleClient>().As<BaseClient>();

            return builder.Build();
        }
    }
}