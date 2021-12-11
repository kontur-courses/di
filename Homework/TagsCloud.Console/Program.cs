using System.Drawing;
using System.Linq;
using Autofac;
using DeepMorphy;
using TagsCloudContainer.BitmapSaver;
using TagsCloudContainer.FileReader;
using TagsCloudContainer.Layouter;
using TagsCloudContainer.Layouter.PointsProviders;
using TagsCloudContainer.Visualizer;
using TagsCloudContainer.WordsConverters;
using TagsCloudContainer.WordsFilter;
using TagsCloudContainer.WordsFrequencyAnalyzers;

namespace TagsCloud.Console
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var appSettings = AppSettings.Parse(args);
            using var container = Configure(appSettings);
            container.Resolve<IConsoleUI>()
                .Run(appSettings);
        }

        internal static IContainer Configure(IAppSettings settings)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<BitmapSaver>().As<IBitmapSaver>();
            builder.RegisterType<TxtFileReader>().As<IFileReader>();
            builder.RegisterType<WordsFrequencyAnalyzer>().As<IWordsFrequencyAnalyzer>();
            builder.RegisterInstance(new LengthFilter(settings.MinWordLength)).As<IWordsFilter>();
            builder.RegisterInstance(new SpeechPartsFilter(settings.SelectedSpeechParts.ToArray())).As<IWordsFilter>();
            builder.RegisterInstance(new MorphAnalyzer()).AsSelf().SingleInstance();
            builder.RegisterType<RussianWordsConverter>().As<IWordsConverter>();
            builder.RegisterType<SpiralPointsProvider>().As<IPointsProvider>();
            builder.RegisterType<PointsProvidersResolver>().As<IPointsProvidersResolver>();
            builder.Register(c => RegisterCloudLayouter(c, settings)).As<ICloudLayouter>();
            builder.RegisterType<RandomColorGenerator>().As<IColorGenerator>();
            builder.Register(_ => new Point(settings.ImageWidth / 2, settings.ImageHeight / 2)).As<Point>();
            builder.RegisterType<ColorGeneratorsResolver>().As<IColorGeneratorsResolver>();
            builder.Register(c => RegisterVisualizerSettings(c, settings)).As<IVisualizerSettings>();
            builder.RegisterType<Visualizer>().As<IVisualizer>();
            builder.RegisterType<FileReadersResolver>().As<IFileReadersResolver>();
            builder.RegisterType<FilterApplyer>().As<IFilterApplyer>().SingleInstance();
            builder.RegisterType<ConsoleUI>().As<IConsoleUI>();
            return builder.Build();
        }

        private static VisualizerSettings RegisterVisualizerSettings(IComponentContext context, IAppSettings settings)
        {
            return new VisualizerSettings(
                context.Resolve<IColorGeneratorsResolver>().Get(settings.ColoringAlgorythm),
                Color.FromName(settings.BackgroundColor),
                new Font(new FontFamily(settings.FontName), settings.FontSize),
                new Size(settings.ImageWidth, settings.ImageHeight));
        }

        private static CloudLayouter RegisterCloudLayouter(IComponentContext context, IAppSettings settings)
        {
            return new CloudLayouter(context.Resolve<IPointsProvidersResolver>().Get(settings.LayoutAlgorythm));
        }
    }
}