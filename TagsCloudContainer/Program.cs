using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Autofac;
using TagsCloudContainer.Core.Generators;
using TagsCloudContainer.Core.Layouters;
using TagsCloudContainer.Data;
using TagsCloudContainer.Data.Processors;
using TagsCloudContainer.Data.Readers;
using TagsCloudContainer.Savers;
using TagsCloudContainer.Visualization;
using TagsCloudContainer.Visualization.Layouts;
using TagsCloudContainer.Visualization.Painters;

namespace TagsCloudContainer
{
    public static class Program
    {
        public static void Main()
        {
            var container = CreateContainer();

            var wordReader = container.ResolveNamed<IWordsFileReader>("txtWordReader");
            var words = wordReader.ReadAllWords("Resources/words.txt").ToArray();
            var boringWords = wordReader.ReadAllWords("Resources/boring_words.txt").ToArray();

            var lowerCaseProcessor = container.ResolveNamed<IWordProcessor>("lowerCaseProcessor");
            var wordFilter = container.ResolveNamed<IWordProcessor>("wordFilter",
                new NamedParameter("boringWords", boringWords));
            words = wordFilter.Process(lowerCaseProcessor.Process(words)).ToArray();

            var familyParam = new NamedParameter("family", FontFamily.GenericMonospace);
            var sizeFactorParam = new NamedParameter("sizeFactor", 100f);
            var wordMeasurer = container.Resolve<IWordMeasurer>(familyParam, sizeFactorParam);
            var layouter = container.Resolve<IRectangleLayouter>();
            var tags = WordCounter.Count(words)
                .Select(word =>
                {
                    var (font, size) = wordMeasurer.Measure(word);
                    return new Tag(word.Value, font, layouter.PutNextRectangle(size));
                })
                .ToArray();

            var textColorParam = new NamedParameter("textColor", Color.Black);
            var fillColorParam = new NamedParameter("fillColor", Color.Transparent);
            var borderColorParam = new NamedParameter("borderColor", Color.Transparent);
            var painter = container.Resolve<IPainter>(textColorParam, fillColorParam, borderColorParam);

            var visualizer = container.Resolve<TagsCloudVisualizer>();
            var image = visualizer.Visualize(painter.Colorize(tags));

            var saver = container.ResolveNamed<IImageSaver>("pngImageSaver");
            saver.Save("cloud.png", image);
        }

        private static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ArchimedeanSpiral>().As<IPointGenerator>();
            builder.RegisterType<CircularCloudLayouter>()
                .WithParameter("center", Point.Empty)
                .As<IRectangleLayouter>();

            builder.RegisterType<TxtWordsFileReader>().Named<IWordsFileReader>("txtWordReader");

            builder.RegisterType<LowerCaseWordProcessor>().Named<IWordProcessor>("lowerCaseProcessor");
            builder.Register((c, p) =>
                    new WordFilter(p.Named<IEnumerable<string>>("boringWords")))
                .Named<IWordProcessor>("wordFilter");

            builder.Register((c, p) => new ProbabilityWordMeasurer(
                    p.Named<FontFamily>("family"), p.Named<float>("sizeFactor")))
                .As<IWordMeasurer>();

            builder.Register((c, p) => new ConstantColorsPainter(
                    p.Named<Color>("textColor"),
                    p.Named<Color>("fillColor"),
                    p.Named<Color>("borderColor")))
                .As<IPainter>();

            builder.RegisterType<TagsCloudVisualizer>().AsSelf();

            builder.RegisterType<PngImageSaver>().Named<IImageSaver>("pngImageSaver");

            return builder.Build();
        }
    }
}