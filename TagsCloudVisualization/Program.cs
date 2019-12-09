using System.Drawing;
using System.Drawing.Imaging;
using Autofac;
using CommandLine;
using TagsCloudVisualization.CloudPainters;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.TextFilters;
using TagsCloudVisualization.TextPreprocessing;
using TagsCloudVisualization.TextReaders;
using TagsCloudVisualization.Visualization;
using TagsCloudVisualization.WordConverters;
using TagsCloudVisualization.PathFinders;

namespace TagsCloudVisualization
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var applicationOptions = GetApplicationOptions(Parser.Default.ParseArguments<ApplicationOptions>(args));
            var cloudCreator = GetContainer(applicationOptions.GetVisualizingOptions()).Resolve<CloudCreator>();
            var cloud = cloudCreator.GetCloud(applicationOptions.TextName);
            ImageSaver.SaveImageToDefaultDirectory(applicationOptions.ImageName, cloud, ImageFormat.Png);
        }

        private static ApplicationOptions GetApplicationOptions(ParserResult<ApplicationOptions> parsedOptions)
        {
            var applicationOptions = new ApplicationOptions();
            parsedOptions.WithParsed(o =>
            {
                applicationOptions.FontFamily = o.FontFamily;
                applicationOptions.FontSize = o.FontSize;
                applicationOptions.ImageHeight = o.ImageHeight;
                applicationOptions.ImageName = o.ImageName;
                applicationOptions.ImageWidth = o.ImageWidth;
                applicationOptions.TextColor = o.TextColor;
                applicationOptions.TextName = o.TextName;
                applicationOptions.BackGroundColor = o.BackGroundColor;
            });
            
            return applicationOptions;
        }

        private static IContainer GetContainer(VisualisingOptions imageOptions)
        {
            var affPath = PathFinder.GetHunspellDictionariesPath("ru_RU.aff");
            var dicPath = PathFinder.GetHunspellDictionariesPath("ru_RU.dic");
            var center = new Point(imageOptions.ImageSize.Width / 2, imageOptions.ImageSize.Height / 2);
            var builder = new ContainerBuilder();
            builder.RegisterType<MultiColorCloudPainter>().As<ICloudPainter>();
            builder.RegisterType<LowerCaseWordConverter>().As<IWordConverter>();
            builder.RegisterType<NormalFormWordConverter>().As<IWordConverter>()
                .WithParameter("affPath", affPath)
                .WithParameter("dicPath", dicPath);
            builder.RegisterType<ShortWordsFilter>().As<ITextFilter>();
            builder.RegisterType<BoringWordsFilter>().As<ITextFilter>();
            builder.RegisterType<RepeatingWordsFilter>().As<ITextFilter>();
            builder.RegisterType<TxtReader>().As<ITextReader>();
            builder.RegisterType<WordsExtractor>().AsSelf();
            builder.RegisterType<WordPreprocessor>().AsSelf();
            builder.RegisterType<TagCloudVisualizer>().AsSelf().WithParameter("visualisingOptions", imageOptions);
            builder.RegisterType<Spiral>().AsSelf().WithParameter("center", center);
            builder.RegisterType<CircularCloudLayouter>().As<ILayouter>();
            builder.RegisterType<CloudCreator>().AsSelf();
            return builder.Build();
        }
    }
}