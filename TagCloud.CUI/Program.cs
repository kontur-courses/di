using Autofac;
using Autofac.Core;
using CommandLine;
using TagCloud.Core.Layouters;
using TagCloud.Core.Painters;
using TagCloud.Core.Settings.DefaultImplementations;
using TagCloud.Core.Settings.Interfaces;
using TagCloud.Core.TextParsing;
using TagCloud.Core.Visualizers;
using TagCloud.Core.WordsParsing.WordsProcessing;
using TagCloud.Core.WordsParsing.WordsProcessing.WordsProcessingUtilities;
using TagCloud.Core.WordsParsing.WordsReading;

namespace TagCloud.CUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            InjectDependencies(builder);

            if (args.Length == 0)
                args = new[]
                {
                    @"-p", @"test_words.txt",
                    @"-i", @"result.bmp",
                    @"maxtagscount", @"10"
                };

            Parser.Default
                .ParseArguments<Options>(args)
                .WithParsed(options => builder.RegisterInstance(options)
                    .As<IPaintingSettings>()
                    .As<IVisualizingSettings>()
                    .As<ITagCloudSettings>()
                    .As<ITextParsingSettings>()
                    .As<ILayoutingSettings>()
                );


            var container = builder.Build();
            var tagCloud = container.Resolve<Core.TagCloud>();
            tagCloud.MakeTagCloudAndSave();
        }

        static void InjectDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<Core.TagCloud>().AsSelf();

            builder.RegisterType<TxtWordsReader>().As<IWordsReader>();
            builder.RegisterType<XmlWordsReader>().As<IWordsReader>();
            builder.RegisterType<GeneralWordsReader>().AsSelf();
            builder.RegisterType<LowerCaseUtility>().As<IWordsProcessingUtility>();
            builder.RegisterType<SimpleWordsProcessor>().As<IWordsProcessor>();
            builder.RegisterType<TextParsingSettings>().As<ITextParsingSettings>().AsSelf().SingleInstance();
            builder.RegisterType<WordsParser>()
                .WithParameter(new ResolvedParameter(
                    (pi, ctx) => pi.ParameterType == typeof(IWordsReader),
                    (pi, ctx) => ctx.Resolve<GeneralWordsReader>()))
                .AsSelf();

            builder.RegisterType<SimpleTagCloudVisualizer>().As<ITagCloudVisualizer>();
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
            builder.RegisterType<OneColorPainter>().As<IPainter>();
        }
    }
}
