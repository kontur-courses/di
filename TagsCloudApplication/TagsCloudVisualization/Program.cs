using System.Drawing;
using TagsCloudLayout.CloudLayouters;
using Autofac;
using TextConfiguration;
using TextConfiguration.TextReaders;
using TagsCloudLayout.PointLayouters;
using TextConfiguration.WordFilters;
using TextConfiguration.WordProcessors;
using CommandLine;

namespace TagsCloudVisualization
{
    public static class Program
    {
        private static void ConfigureConsolePropertiesProvider(this ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<ConsoleTagCloudBuilderPropertiesProvider>();
            containerBuilder.Register(c => c.Resolve<ConsoleTagCloudBuilderPropertiesProvider>()
                .GetCloudTagProperties())
                .As<CloudTagProperties>();
            containerBuilder.Register(c => c.Resolve<ConsoleTagCloudBuilderPropertiesProvider>()
                .GetVisualizatorProperties())
                .As<VisualizatorProperties>();
            containerBuilder.Register(c => c.Resolve<ConsoleTagCloudBuilderPropertiesProvider>()
                .GetConstantTextColorProvider())
                .As<ITextColorProvider>();
            containerBuilder.Register(c => c.Resolve<ConsoleTagCloudBuilderPropertiesProvider>()
                .GetIOSettings())
                .As<ITagCloudBuilderProperties>();
            containerBuilder.Register(c => c.Resolve<ConsoleTagCloudBuilderPropertiesProvider>()
                .GetCentralPoint())
                .As<Point>();
        }

        private static IContainer ConfigureContainer(Options opts)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterInstance(opts)
                .As<Options>();
            containerBuilder.ConfigureConsolePropertiesProvider();

            containerBuilder.RegisterType<RawTextReader>()
                .As<ITextReader>();
            containerBuilder.RegisterType<BoringWordsFilter>()
                .As<IWordFilter>();
            containerBuilder.RegisterType<EmptyWordFilter>()
                .As<IWordFilter>();
            if (opts.BoringWordsFilename != null)
                containerBuilder.Register(c =>
                new CustomBoringWordsFilter(
                    c.Resolve<ITextReader>(),
                    c.Resolve<Options>().BoringWordsFilename))
                .As<IWordFilter>();
            containerBuilder.RegisterType<ToLowerCaseProcessor>()
                .As<IWordProcessor>();
            containerBuilder.RegisterType<TextPreprocessor>()
                .As<ITextPreprocessor>();
            containerBuilder.RegisterType<WordsProvider>()
                .As<IWordsProvider>();
            containerBuilder.RegisterType<CloudTagProvider>()
                .As<ITagProvider>();

            containerBuilder.RegisterType<ArchimedeanSpiral>()
                .As<ICircularPointLayouter>();
            containerBuilder.RegisterType<CircularCloudLayouter>()
                .As<ICloudLayouter>();

            containerBuilder.RegisterType<TagCloudVisualizator>()
                .As<ITagCloudVisualizator>();

            containerBuilder.RegisterType<ConsoleTagCloudBuilder>();

            return containerBuilder.Build();
        }

        private static void RunWithParsedOptions(Options opts)
        {
            var container = ConfigureContainer(opts);
            container.Resolve<ConsoleTagCloudBuilder>().Run();
        }

        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
            .WithParsed(opts => RunWithParsedOptions(opts));
        }
    }
}
