using System.Collections.Generic;
using System.Drawing;
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
            var textName = "text.txt";
            var imageName = "exampleMultiColor";
            var container = GetContainer(font, imageSize, backGroundColor, textColor);
            var text = new TextReader(textName).GetText();
            var words = new TextPreparer(text).GetWords();
            var preprocessedWords =
                new WordPreprocessor(words).GetPreprocessedWords(container.Resolve<IEnumerable<ITextFilter>>());
            var visualizer = container.Resolve<TagCloudVisualizer>();
            var image = visualizer.GetVisualization(preprocessedWords, container.Resolve<ILayouter>(),
                container.Resolve<Painter>());
            ImageSaver.SaveImageToDefaultDirectory(imageName, image);
        }

        private static IContainer GetContainer(Font font, Size imageSize,
            Color backgroundColor, Color textColor)
        {
            var center = new Point(imageSize.Width / 2, imageSize.Height / 2);
            var builder = new ContainerBuilder();
            builder.Register(f => font).As<Font>();
            builder.Register(s => imageSize).As<Size>();
            builder.RegisterType<MultiColorPainter>().As<Painter>();
            builder.Register(f => new List<ITextFilter> {new ShortWordsFilter(3), new RepeatingWordsFilter()})
                .As<IEnumerable<ITextFilter>>();
            builder.Register(p => center).As<Point>();
            builder.RegisterType<CircularCloudLayouter>().As<ILayouter>();
            builder.RegisterType<Options>().AsSelf().WithParameter("backgroundColor", backgroundColor)
                .WithParameter("textColor", textColor);
            builder.RegisterType<TagCloudVisualizer>().AsSelf();
            return builder.Build();
        }
    }
}