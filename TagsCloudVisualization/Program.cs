using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using Autofac;
using TagsCloudVisualization.texts;

namespace TagsCloudVisualization
{
    public static class Program
    {
        public static void Main()
        {
            var backGroundColor = Color.Black;
            var textColor = Color.Pink;
            var font = new Font("Arial", 10, FontStyle.Bold);
            var imageSize = new Size(800, 800);
            var textName = "3.txt";
            var imageName = "e1";
            var container = GetContainer(font, imageSize, backGroundColor, textColor, textName);
            container.CreateCloud(imageName, ImageFormat.Png);
        }

        private static IContainer GetContainer(Font font, Size imageSize,
            Color backgroundColor, Color textColor, string textFileName)
        {
            var center = new Point(imageSize.Width / 2, imageSize.Height / 2);
            var builder = new ContainerBuilder();
            builder.Register(f => font).As<Font>();
            builder.Register(s => imageSize).As<Size>();
            builder.RegisterType<MultiColorPainter>().As<Painter>();
            builder.RegisterType<ITextFilter>().AsImplementedInterfaces();
            builder.Register(f => new List<ITextFilter> {new ShortWordsFilter(3), new RepeatingWordsFilter()})
                .As<IEnumerable<ITextFilter>>();
            builder.Register(p => center).As<Point>();
            builder.RegisterType<CircularCloudLayouter>().As<ILayouter>();
            builder.RegisterType<VisualisingOptions>().AsSelf().WithParameter("backgroundColor", backgroundColor)
                .WithParameter("textColor", textColor);
            builder.RegisterType<TagCloudVisualizer>().AsSelf();
            builder.RegisterType<TxtReader>().As<ITextReader>().WithParameter("fileName", textFileName);
            return builder.Build();
        }

        private static void CreateCloud(this IContainer container, string imageName, ImageFormat format)
        {
            var text = container.Resolve<ITextReader>().GetText();
            var words = new TextPreparer(text).GetWords();
            var preprocessedWords =
                new WordPreprocessor(words, container.Resolve<IEnumerable<ITextFilter>>()).GetPreprocessedWords();
            var visualizer = container.Resolve<TagCloudVisualizer>();
            var image = visualizer.GetVisualization(preprocessedWords, container.Resolve<ILayouter>(),
                container.Resolve<Painter>());
            ImageSaver.SaveImageToDefaultDirectory(imageName, image, format);
        }
    }
}