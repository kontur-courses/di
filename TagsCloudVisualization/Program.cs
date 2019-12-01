using System;
using System.Collections.Generic;
using System.Drawing;
using Autofac;

namespace TagsCloudVisualization
{
    public static class Program
    {
        public static void Main()
        {
            var backGroundColor = Color.Black;
            var textColor = Color.Pink;
            var font = new Font("Arial", 10, FontStyle.Bold);
            var imageSize = new Size(600, 600);
            //var painter = new MultiColorPainter(imageSize);
            var painter = new SingleColorPainter(imageSize);
            var filters = new List<ITextFilter> {new ShortWordsFilter(3)};
            var textName = "2.txt";
            var imageName = "exampleSingleColor";
            var words = new TextReader(textName).GetWords();
            var preprocessedWords = new WordPreprocessor(words).GetPreprocessedWords(filters);
            var container = GetContainer(font, imageSize, filters, backGroundColor, textColor);
            var visualizer = container.Resolve<TagCloudVisualizer>();
            var image = visualizer.GetVisualization(preprocessedWords, container.Resolve<ILayouter>(), painter);
            ImageSaver.SaveImageToDefaultDirectory(imageName, image);
        }

        private static IContainer GetContainer(Font font, Size imageSize, IEnumerable<ITextFilter> filters,
            Color backgroundColor, Color textColor)
        {
            var center = new Point(imageSize.Width / 2, imageSize.Height / 2);
            var builder = new ContainerBuilder();
            builder.Register(f => font).As<Font>();
            builder.Register(s => imageSize).As<Size>();
            builder.Register(f => filters).As<IEnumerable<ITextFilter>>();
            builder.Register(p => center).As<Point>();
            builder.RegisterType<CircularCloudLayouter>().As<ILayouter>();
            builder.RegisterType<Options>().AsSelf().WithParameter("backgroundColor", backgroundColor)
                .WithParameter("textColor", textColor);
            builder.RegisterType<TagCloudVisualizer>().AsSelf();
            return builder.Build();
        }
    }
}