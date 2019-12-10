using System.Drawing;
using TagsCloudLayout.CloudLayouters;
using Autofac;
using TextConfiguration;
using TextConfiguration.TextReaders;
using TagsCloudLayout.PointLayouters;
using TextConfiguration.WordFilters;
using TextConfiguration.WordProcessors;
using CommandLine;
using System.Drawing.Imaging;

namespace TagsCloudVisualization
{
    public static class Program
    {
        private static IContainer ConfigureContainer(Options opts)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterInstance(opts)
                .As<Options>();
            containerBuilder.RegisterInstance(
                new CloudTagProperties(new FontFamily(opts.FontFamilyName), opts.FontSize))
                .As<CloudTagProperties>();
            containerBuilder.RegisterInstance(
                new VisualizatorProperties(new Size(opts.ImageSize[0], opts.ImageSize[1])))
                .As<VisualizatorProperties>();
            containerBuilder.RegisterInstance(
                new ConstantTextColorProvider(Color.FromName(opts.FontColorName)))
                .As<ITextColorProvider>();
            containerBuilder.Register(c => new ImageSaver(c.Resolve<Options>().OutputImageFormatName))
                .As<ImageSaver>();
            containerBuilder.Register(c => new Point(opts.CentralPoint[0], opts.CentralPoint[1]))
                .As<Point>();

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
            containerBuilder.RegisterType<TextPreprocessor>();
            containerBuilder.RegisterType<WordsProvider>();
            containerBuilder.RegisterType<CloudTagProvider>();

            containerBuilder.RegisterType<ArchimedeanSpiral>()
                .As<ICircularPointLayouter>();
            containerBuilder.RegisterType<CircularCloudLayouter>()
                .As<ICloudLayouter>();

            containerBuilder.RegisterType<TagCloudVisualizator>();

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
