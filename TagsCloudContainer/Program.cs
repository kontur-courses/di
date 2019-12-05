using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using Autofac;
using TagsCloudContainer.Core.Generators;
using TagsCloudContainer.Core.Layouters;
using TagsCloudContainer.Data;
using TagsCloudContainer.Data.Processors;
using TagsCloudContainer.Data.Readers;
using TagsCloudContainer.Visualization;
using TagsCloudContainer.Visualization.Layouts;
using TagsCloudContainer.Visualization.Painters;

namespace TagsCloudContainer
{
    public class Program
    {
        public static void Main()
        {
            const string boringWordsPath = "Resources/boring_words.txt";
            const string wordsPath = "Resources/words.txt";

            var container = CreateContainer();

            var boringWordsPathParam = new NamedParameter("boringWordsPath", boringWordsPath);
            var wordsPathParam = new NamedParameter("wordsPath", wordsPath);
            var wordReader = container.Resolve<IWordReader>(wordsPathParam, boringWordsPathParam);
            var words = wordReader.ReadAllWords().ToArray();

            var layouter = container.Resolve<CircularCloudLayouter>();

            var family = FontFamily.GenericMonospace;
            const float sizeFactor = 100;

            var familyParam = new NamedParameter("family", family);
            var sizeFactorParam = new NamedParameter("sizeFactor", sizeFactor);
            var wordsLayout = container.Resolve<IWordsLayout>(familyParam, sizeFactorParam);
            var tags = wordsLayout.PlaceWords(layouter, WordCounter.Count(words)).ToArray();

            var textColor = Color.Black;
            var fillColor = Color.Transparent;
            var borderColor = Color.Transparent;

            var textColorParam = new NamedParameter("textColor", textColor);
            var fillColorParam = new NamedParameter("fillColor", fillColor);
            var borderColorParam = new NamedParameter("borderColor", borderColor);
            var painter = container.Resolve<IPainter>(textColorParam, fillColorParam, borderColorParam);

            var visualizer = container.Resolve<CircularCloudVisualizer>();
            var image = visualizer.Visualize(painter, tags);
            var format = ImageFormat.Png;
            var extension = new ImageFormatConverter().ConvertToString(format)?.ToLower();
            image.Save($"tags_cloud.{extension}", format);
        }

        private static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ArchimedeanSpiral>().As<IPointGenerator>();
            builder.RegisterType<CircularCloudLayouter>()
                .WithParameter("center", Point.Empty)
                .AsSelf();

            builder.Register((c, p) => new WordListReader(p.Named<string>("boringWordsPath")))
                .Named<WordListReader>("boringWordReader");
            builder.Register((c, p) => new WordListReader(p.Named<string>("wordsPath")))
                .As<IWordReader>();

            builder.RegisterDecorator<LowerCaseTransformer, IWordReader>();
            builder.RegisterDecorator<IWordReader>((c, p, i) =>
                new WordFilter(i, c.ResolveNamed<WordListReader>("boringWordReader", p).ReadAllWords()));

            builder.Register((c, p) => new CentricWordsLayout(p.Named<FontFamily>("family"),
                    p.Named<float>("sizeFactor")))
                .As<IWordsLayout>();

            builder.Register((c, p) => new ConstantColorsPainter(
                    p.Named<Color>("textColor"),
                    p.Named<Color>("fillColor"),
                    p.Named<Color>("borderColor")))
                .As<IPainter>();

            builder.RegisterType<CircularCloudVisualizer>().AsSelf();

            return builder.Build();
        }
    }
}