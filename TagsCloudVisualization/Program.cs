using System.Collections.Generic;
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

namespace TagsCloudVisualization
{
    public static class Program
    {
        public static void Main()
        {
            var imageOptions = new VisualisingOptions(new Font("Arial", 10, FontStyle.Bold), 
                new Size(600, 600), Color.Black, Color.Pink);
            var textName = "2.txt";
            var imageName = "00";

            var container = GetContainer(imageOptions.Font, imageOptions.ImageSize, imageOptions.BackgroundColor,
                imageOptions.TextColor);
            var creator = container.Resolve<CloudCreator>();
            var cloud = creator.GetCloud(imageOptions.BackgroundColor, imageOptions.TextColor, imageOptions.Font,
                imageOptions.ImageSize, textName);
            ImageSaver.SaveImageToDefaultDirectory(imageName, cloud, ImageFormat.Png);
        }

        private static IContainer GetContainer(Font font, Size imageSize,
            Color backgroundColor, Color textColor)
        {
            var center = new Point(imageSize.Width / 2, imageSize.Height / 2);

            var builder = new ContainerBuilder();
            builder.Register(f => font).As<Font>();
            builder.Register(s => imageSize).As<Size>();
            builder.RegisterType<Spiral>().AsSelf().WithParameter("center", center);
            builder.RegisterType<MultiColorCloudPainter>().As<CloudPainter>();
            builder.RegisterType<ITextFilter>().AsImplementedInterfaces();
            builder.RegisterType<LowerCaseWordConverter>().AsSelf();
            builder.Register(f => new List<IWordConverter> {new LowerCaseWordConverter()});
            builder.Register(f => new List<ITextFilter> {new ShortWordsFilter(3)})
                .As<IEnumerable<ITextFilter>>();
            builder.Register(p => center).As<Point>();
            builder.RegisterType<CircularCloudLayouter>().As<ILayouter>();
            builder.RegisterType<VisualisingOptions>().AsSelf().WithParameter("backgroundColor", backgroundColor)
                .WithParameter("textColor", textColor);
            builder.RegisterType<TagCloudVisualizer>().AsSelf();
            builder.RegisterType<TxtReader>().As<ITextReader>();
            builder.RegisterType<WordsProvider>().AsSelf();
            builder.RegisterType<WordPreprocessor>().AsSelf();
            builder.RegisterType<CloudCreator>().AsSelf();
            return builder.Build();
        }
    }
}