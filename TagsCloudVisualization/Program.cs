using System.Drawing;
using System.Drawing.Imaging;
using Autofac;
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
        public static void Main()
        {
            var imageOptions = new VisualisingOptions(new Font("Arial", 10, FontStyle.Bold),
                new Size(600, 600), Color.Black, Color.Pink);
            var textName = "2.txt";
            var imageName = "01";
            var cloudCreator = GetContainer(imageOptions).Resolve<CloudCreator>();
            var cloud = cloudCreator.GetCloud(textName);
            ImageSaver.SaveImageToDefaultDirectory(imageName, cloud, ImageFormat.Png);
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